using System.Security.Claims;
using MyCinema.Data;
using MyCinema.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCinema.Services;
using MyCinema.Services.IServices;

namespace MyCinema.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        //public async Task<IActionResult> Index()
        //{

        //}
        public async Task<IActionResult> MoviesList()
        {
            var movies = await _adminService.GetAllMoviesWithPhotosAsync();
            return View(movies);
        }
        public IActionResult AddSalon()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSalon(TheatreSalon salon, string clickedCells)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _adminService.AddSalonAsync(salon, clickedCells);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("SalonNumber", ex.Message);
                }
            }
            return RedirectToAction("AddSalon");
        }
        public IActionResult AddMovie()
        {
            var viewData = _adminService.GetAddMovieViewDataAsync();
            ViewData["Lange"] = viewData.Result.Languages;
            ViewData["Genre"] = new SelectList(viewData.Result.Genres, "Value", "Text");
            ViewData["Subs"] = new SelectList(viewData.Result.Subtitles, "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie(Movie movie, List<IFormFile> MoviePhotos)
        {
            await _adminService.AddMovieWithPhotosAsync(movie, MoviePhotos);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Salons()
        {
            var salons = await _adminService.GetTheatreSalonsAsync();
            return View(salons);
        }
    }
}
