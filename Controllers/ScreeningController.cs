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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
    CreateScreening(AddScreeningViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _screeningService.AddScreeningInDbAsync(model);
                    return RedirectToAction("Index", "Screening");
                }
                catch (InvalidOperationException ex)
                {

                    ViewData["ErrorMessage"] = ex.Message;
                    return View(model);
                }
            }
            model.TheatreSalons = await _screeningService.GetSalonsAsync();
            model.Movies = await _screeningService.GetMoviesAsync();
            return View(model);
        }
        public async Task<IActionResult> GetByDate(string movieId, string date)
        {
            Guid.TryParse(movieId, out Guid id);
            DateTime.TryParse(date, out DateTime d);
            var screenings = await _screeningService.GetMovieDetailsViewModelViewModelByMovieAndDate(id, d);
            return PartialView("_ScreeningsPartial", screenings);
        }
    }
}
