using MyCinema.Data;

namespace MyCinema.Services.IServices
{
    public interface IHomeService
    {
        public Task<List<Movie>> GetAllMoviesWithPhotosAsync();
    }
}
