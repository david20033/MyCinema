using MyCinema.Data;
using MyCinema.Data.MovieApi;
using MyCinema.Migrations;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
using RestSharp;

namespace MyCinema.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IApiService _apiService;
        private readonly ILanguageRepository _languageRepository;
        public MovieService(IMovieRepository movieRepository, IApiService apiService, ILanguageRepository languageRepository)
        {
            _movieRepository = movieRepository;
            _apiService = apiService;
            _languageRepository = languageRepository;
        }
        public  async Task<Movie> GetMovieWithPhotosByIdAsync(Guid id)
        {
            return await _movieRepository.GetMovieWithPhotosByIdAsync(id);
        }
        public async Task<List<MovieNowPlayingDTO>> GetNowPlayingMoviesAsync()
        {
            try
            {
                var movies = await _apiService.GetNowPlayingMoviesAsync();
                foreach(var movie in movies)
                {
                    movie.original_language =await _languageRepository.GetLanguageNameByIsoCodeAsync(movie.original_language);
                }
                return movies;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrieve now playing movies.", ex);
            }
        }
    }
}
