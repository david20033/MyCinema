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
            var model = await _ticketService.GetTicketOrderByIdAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectSeats(TicketOrder model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}
