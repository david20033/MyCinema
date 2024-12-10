using Microsoft.AspNetCore.Mvc;
using MyCinema.Data;

namespace MyCinema.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyCinemaDBContext _context;
        public AdminController(MyCinemaDBContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddMovie()
        {
            return View();
        }
    }
}
