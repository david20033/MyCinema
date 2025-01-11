using MyCinema.Data;

namespace MyCinema.Services.IServices
{
    public interface IScreeningService
    {
         Task<List<TheatreSalon>> GetSalonsAsync();
         Task<List<Movie>> GetMoviesAsync();

    }
}
