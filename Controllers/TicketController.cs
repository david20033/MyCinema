using Microsoft.AspNetCore.Mvc;
using MyCinema.Services.IServices;

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
    }
}
