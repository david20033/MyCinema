using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCinema.Data;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;

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
            return View(model);
        }
        public async Task<IActionResult> SelectSeats(Guid id)
        {
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _ticketService.AddUserIdInTickerOrder(OrderId, userId);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
