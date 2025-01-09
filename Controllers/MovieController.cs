using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCinema.Data;
using MyCinema.Services;
using MyCinema.Services.IServices;

namespace MyCinema.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MoviesList()
        {
            var movies = await _movieService.GetAllMoviesWithPhotosAsync();
            return View(movies);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _movieService.GetMovieWithPhotosByIdAsync(id));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddMovie()
        {
            var viewData = _movieService.GetAddMovieViewDataAsync();
            ViewData["Lange"] = viewData.Result.Languages;
            ViewData["Genre"] = new SelectList(viewData.Result.Genres, "Value", "Text");
            ViewData["Subs"] = new SelectList(viewData.Result.Subtitles, "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie(Movie movie, List<IFormFile> MoviePhotos)
        {
            await _movieService.AddMovieWithPhotosAsync(movie, MoviePhotos);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> NowPlaying(int page)
        {
            if (page == 0) page = 1;
            ViewBag.currentPage = page;
            ViewBag.totalPages = 100; //will be changed later
            var movies = await _movieService.GetNowPlayingMoviesAsync(page);
            return View(movies);
        }
    }
}
