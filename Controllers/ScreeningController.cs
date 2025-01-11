using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;

namespace MyCinema.Controllers
{
    public class ScreeningController : Controller
    {
        private readonly IScreeningService _screeningService;
        public ScreeningController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateScreening()  
        {
            var model = new AddScreeningViewModel
            {
                TheatreSalons = await _screeningService.GetSalonsAsync(),
                Movies = await _screeningService.GetMoviesAsync()
            };

            return View(model);
        }
        //[Authorize(Roles ="Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public Task<IActionResult> CreateScreening()
        //{

        //}
    }
}
