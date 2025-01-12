using MyCinema.Data;

namespace MyCinema.Repositories.IRepositories
{
    public interface IMovieRepository
    {
        Task AddMovieAsync(Movie movie);
        Task AddMoviePhotoAsync(MoviePhoto moviePhoto);
        Task<List<Movie>> GetAllMoviesWithPhotosAsync();
        Task<Movie> GetMovieWithPhotosByIdAsync(Guid id);
        Task<Movie> GetMovieDetailsByIdAsync(Guid id);
        Task<List<Movie>> GetMoviesAsync(int pageNumber);
        Task<int> GetMoviesCount();
        Task<List<Movie>> GetAllMoviesAsync();
        Task<List<Movie>> GetAllUnprojectedScreenings();
    }
}
