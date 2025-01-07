using MyCinema.Data;

namespace MyCinema.Services.IServices
{
    public interface IMovieService
    {
        Task<Movie> GetMovieWithPhotosByIdAsync(Guid id);
    }
}
