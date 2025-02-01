using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Data.MovieApi;
using MyCinema.Data.MovieApi.Responses;
using MyCinema.Enums;
using MyCinema.Helpers;
using MyCinema.Repositories;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
using MyCinema.Services.Mappers.IMappers;
using MyCinema.ViewModels;
using RestSharp;

namespace MyCinema.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IApiService _apiService;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMovieMapper _movieMapper;
        private readonly MyCinemaDBContext _context;
        private readonly EnumServices _enumServices;
        public MovieService(IMovieRepository movieRepository, IApiService apiService, IMovieMapper movieMapper, ILanguageRepository languageRepository, MyCinemaDBContext context, EnumServices enumServices)
        {
            _movieRepository = movieRepository;
            _apiService = apiService;
            _movieMapper = movieMapper;
            _languageRepository = languageRepository;
            _context = context;
            _enumServices = enumServices;
        }
        public  async Task<Movie> GetMovieWithPhotosByIdAsync(Guid id)
        {
            return await _movieRepository.GetMovieWithPhotosByIdAsync(id);
        }
        public async Task<List<MovieNowPlayingDTO>> GetNowPlayingMoviesAsync(int page)
        {
            try
            {
                var movies = await _apiService.GetNowPlayingMoviesAsync(page);
                foreach(var movie in movies)
                {
                    var OriginalLanguage = await _languageRepository.GetLanguageNameByIsoCodeAsync(movie.original_language);
                    if (OriginalLanguage != null)
                    {
                        movie.original_language=OriginalLanguage?.English_Name;
                    }
                }
                return movies;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrieve now playing movies.", ex);
            }
        }
        public async Task AddMovieWithPhotosAsync(Movie movie, List<IFormFile> MoviePhotos)
        {
            await _movieRepository.AddMovieAsync(movie);
            if (MoviePhotos != null && MoviePhotos.Any())
            {
                foreach (var file in MoviePhotos)
                {
                    if (file.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);

                        var MoviePhoto = new MoviePhoto
                        {
                            Id = Guid.NewGuid(),
                            MovieId = movie.Id,
                            Data = memoryStream.ToArray(),
                            ContentType = file.ContentType,
                        };
                        await _movieRepository.AddMoviePhotoAsync(MoviePhoto);
                    }
                }
            }
        }
        public async Task<AddMovieViewModel> GetAddMovieViewDataAsync()
        {
            var genres = await _context.Genre.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToListAsync();

            var languages = _enumServices.GetEnumSelectList<Langue>();

            var subtitles = new List<SelectListItem>
        {
            new SelectListItem { Text = "Yes", Value = "true" },
            new SelectListItem { Text = "No", Value = "false" }
        };

            return new AddMovieViewModel
            {
                Genres = genres,
                Languages = languages,
                Subtitles = subtitles
            };
        }
        public async Task<List<Movie>> GetAllMoviesWithPhotosAsync()
        {
            return await _movieRepository.GetAllMoviesWithPhotosAsync();
        }
        public async Task<List<Movie>> GetMoviesAsync(int page)
        {
            return await _movieRepository.GetMoviesAsync(page);
        }
        public async Task<MovieWithCreditsDTO> GetMovieDetailsByIdFromAPI(int id)
        {
            return await _apiService.GetMovieWithCreditsByIdDTO(id);
        }
        public async Task<Movie> GetMovieDetailsByIdFromDb(Guid id)
        {
            return await _movieRepository.GetMovieDetailsByIdAsync(id);
        }
        public async Task AddMovieRangeInDataBaseByIds(List<int> movies)
        {
            foreach (var Movieid in movies)
            {
                var movieDTO = _apiService.GetMovieDetailsByIdAsync(Movieid).Result;
                var movieCastList = _apiService
                    .GetMovieCreditsByIdAsync(Movieid)
                    .Result.cast?
                    .Select(m=>m.name)
                    .Take(10)
                    .ToList();
                var movieCrewList = _apiService
                    .GetMovieCreditsByIdAsync(Movieid)
                    .Result.crew?
                    .Where(m=>m.job == "Director")
                    .Select(m => m.name)
                    .Take(5)
                    .ToList();
                if (movieDTO == null)
                {
                    continue;
                }
                
                var movieEntity = await _movieMapper.MapMovieDTOToEntity(movieDTO,movieCastList ,movieCrewList);
                await _movieRepository.AddMovieAsync(movieEntity);
            }
        }    
        public MovieDetailsViewModel MovieDetailsViewModel(Movie movie, MovieWithCreditsDTO MovieDTO)
        {
            return _movieMapper.MapToMovieDetailViewModel(movie, MovieDTO);
        }
        public async Task<List<MovieListViewModel>> GetMovieListViewModelFromDb (int page)
        {
            var list = new List<MovieListViewModel>();
            var movies = await _movieRepository.GetMoviesAsync(page);
            var movieDTO = new MovieNowPlayingDTO();
            foreach (var m in movies)
            {
                list.Add(await _movieMapper.MapToMovieListViewModel(m, movieDTO));
            }
            return list;
        }
        public async Task<List<MovieListViewModel>> GetMovieListViewModelFromApi(int page)
        {
            var list = new List<MovieListViewModel>();
            var movies = await _apiService.GetNowPlayingMoviesAsync(page);
            var movieEntity = new Movie();
            foreach (var m in movies)
            {
                list.Add(await _movieMapper.MapToMovieListViewModel(movieEntity, m));
            }
            return list;
        }
        public async Task<int> GetMoviesCount()
        {
            return await _movieRepository.GetMoviesCount();
        }
        public async Task<List<MovieScreeningViewModel>> GetMovieScreeningViewModelsAsync(QueryObject query)
        {
            if (query.Date == null)
            {
                query.Date = DateTime.Now;
            }
            var result = new List<MovieScreeningViewModel>();
            var screening = await _movieRepository.GetAllUnprojectedScreenings(query);
            foreach (var s in screening)
            {
                var model = new MovieScreeningViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Overview = s.Overview,
                    Genres = s.Genres?.ToList(),
                    RunTime = s.Runtime,
                    Poster_path = s.Poster_path,
                    isAdult = s.Adult,
                    Screenings = s.Screenings
                };
                result.Add(model);
            }
            return result;
        }

    }
}
