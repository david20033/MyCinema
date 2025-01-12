using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCinema.Data;
using MyCinema.Data.MovieApi;
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
        public async Task<IActionResult> Index()
        {
            var screenings = await _movieService.GetMovieScreeningViewModelsAsync();
            return View(screenings);
        }
        public async Task<IActionResult> Details(string id)
        {
            Guid result;
            Movie Movie = new Movie();
            MovieWithCreditsDTO MovieDTO = new MovieWithCreditsDTO();
            MovieCreditsResponseDTO MovieCreditsResponse = new MovieCreditsResponseDTO();
            MovieResponseDTO MovieResponseDTO = new MovieResponseDTO();
            MovieDTO.Movie = MovieResponseDTO;
            MovieDTO.Credits = MovieCreditsResponse;
            ViewBag.BackButton = null;
            if (Guid.TryParse(id, out result))
            {
                Movie = await _movieService.GetMovieDetailsByIdFromDb(result);
                ViewBag.BackButton = "MovieList";
            }
            else
            {
                MovieDTO = await _movieService.GetMovieDetailsByIdFromAPI(int.Parse(id));
                ViewBag.BackButton = "NowPlaying";
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
            return View(await _movieService.GetMovieListViewModelFromApi(page));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MovieList(int page)
        {
            if (page == 0) page = 1;
            ViewBag.currentPage = 1;
            var totalMovies = await _movieService.GetMoviesCount();
            ViewBag.totalPages = Math.Ceiling((decimal) totalMovies/20); 
            return View(await _movieService.GetMovieListViewModelFromDb(page));
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
