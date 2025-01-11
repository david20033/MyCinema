using MyCinema.Data;
using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface IScreeningService
    {
         Task<List<TheatreSalon>> GetSalonsAsync();
         Task<List<Movie>> GetMoviesAsync();
        Task AddScreeningInDbAsync(AddScreeningViewModel model);

    }
}
