using Microsoft.AspNetCore.Mvc;

namespace MyCinema.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SelectTicket()
        {
            return View();
        }
    }
}
