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
        public async Task<IActionResult> Index()
        {
            var movies= await _adminService.GetAllMoviesWithPhotosAsync();
            return View(movies);
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
    }
}
