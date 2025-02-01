using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCinema.Data;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;
using Stripe.Checkout;

namespace MyCinema.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SelectTicket(Guid id)
        {
            var SelectTicketViewModel = await _ticketService.GetSelectTicketViewModel(id);
            return View(SelectTicketViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectTicket(SelectTicketViewModel model)
        {
            if(ModelState.IsValid)
            {
                var TicketId = await _ticketService.AddTicketOwnershipInDbAsync(model);
                return RedirectToAction("SelectSeats", new { Id = TicketId });
            }
            var SelectTicketViewModel = await _ticketService.GetSelectTicketViewModel(model.ScreeningId);
            return View(SelectTicketViewModel);
        }
        public async Task<IActionResult> SelectSeats(Guid id)
        {
            await _ticketService.UnSeedSeatsCoordsWithTicketOrder(id);
            var model = await _ticketService.GetSelectSeatsViewModel(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectSeats(SelectSeatsViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.SeedSeatsCoordsWithTicketOrder(model);
                return RedirectToAction("ConfirmOrder", new { Id = model.TicketOrderId });
            }
            return View();
        }
        public async Task<IActionResult> ConfirmOrder(Guid Id) 
        {
            var model = await _ticketService.GetConfirmOrderViewModel(Id);
            if (model == null)
            {
                return RedirectToAction("Index", "Movie");
            }
            if (model.RegularTicketSeatsCoords.Contains("-1") || model.VipTicketSeatsCoords.Contains("-1"))
            {
                return RedirectToAction("SelectSeats", new { id = Id });
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder(ConfirmOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var OrderId = model.TicketOrderId;
                if (await _ticketService.IsTicketOrderExists(OrderId) == false)
                {
                    return RedirectToAction("Index");
                }
                var session = await _ticketService.CreateStripeSession(OrderId);
                return Redirect(session.Url);
            }
            return View(model);
        }
        public async Task<IActionResult> Success([FromQuery]Guid OrderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _ticketService.ConfirmTicketOrder(OrderId, userId);
            return RedirectToAction("Index","Movie");
        }
        public IActionResult Cancel([FromQuery]Guid OrderId)
        {
            return RedirectToAction("ConfirmOrder" , new {Id=OrderId });
        }
    }
}
