using MyCinema.Data;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;

namespace MyCinema.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public  async Task<Movie> GetMovieWithPhotosByIdAsync(Guid id)
        {
            return await _movieRepository.GetMovieWithPhotosByIdAsync(id);
        }
    }
}
