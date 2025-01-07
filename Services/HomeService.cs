using MyCinema.Data;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;

namespace MyCinema.Services
{
    public class HomeService : IHomeService
    {
        private readonly IMovieRepository _movieRepository;
        public HomeService(IMovieRepository movieRepository) 
        {
            _movieRepository = movieRepository;
        }
        public async Task<List<Movie>> GetAllMoviesWithPhotosAsync() 
        {
            return await _movieRepository.GetAllMoviesWithPhotosAsync();
        }
    }
}
