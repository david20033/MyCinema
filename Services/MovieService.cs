using MyCinema.Data;
using MyCinema.Data.MovieApi;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
using RestSharp;

namespace MyCinema.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IApiService _apiService;
        public MovieService(IMovieRepository movieRepository, IApiService apiService)
        {
            _movieRepository = movieRepository;
            _apiService = apiService;
        }
        public  async Task<Movie> GetMovieWithPhotosByIdAsync(Guid id)
        {
            return await _movieRepository.GetMovieWithPhotosByIdAsync(id);
        }
        public async Task<List<MovieNowPlayingDTO>> GetNowPlayingMoviesAsync()
        {
            try
            {
                return await _apiService.GetNowPlayingMoviesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrieve now playing movies.", ex);
            }
        }
    }
}
