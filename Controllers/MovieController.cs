using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCinema.Data;
using MyCinema.Data.MovieApi.Responses;
using MyCinema.Services;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;

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
        public async Task<IActionResult> MovieList()
        {
            var movies = await _movieService.GetAllMoviesWithPhotosAsync();
            return View(movies);
        }
        public async Task<IActionResult> Details(string id)
        {
            Guid result;
            Movie Movie = new Movie();
            MovieResponseDTO MovieDTO = new MovieResponseDTO();
            if (Guid.TryParse(id, out result))
            {
                Movie = await _movieService.GetMovieDetailsByIdFromDb(result);
            }
            else
            {
                MovieDTO = await _movieService.GetMovieDetailsByIdFromAPI(int.Parse(id));
            }
         
            return View(_movieService.MovieDetailsViewModel(Movie,MovieDTO));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMoviesToDataBase(string selectedMovies)
        {
            var selectedMovieIds = selectedMovies.Split(',').Select(int.Parse).ToList();
            await _movieService.AddMovieRangeInDataBaseByIds(selectedMovieIds);
            return RedirectToAction("MovieList", "Movie");
        }
    }
}
