using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _movieService.GetMovieWithPhotosByIdAsync(id));
        }
        public async Task<IActionResult> NowPlaying()
        {
            var movies = await _movieService.GetNowPlayingMoviesAsync();
            return Json(movies);
        }
    }
}
