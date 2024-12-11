using MyCinema.Data;

namespace MyCinema.Repositories.IRepositories
{
    public interface IMovieRepository
    {
        Task AddMovieAsync(Movie movie);
        Task AddMoviePhotoAsync(MoviePhoto moviePhoto);
        Task<List<Movie>> GetAllMoviesWithPhotosAsync();
    }
}
