using MyCinema.Data;
using MyCinema.Helpers;
using MyCinema.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyCinema.Services.IServices
{
    public interface ISalonService
    {
        Task AddSalonAsync(TheatreSalon salon, string clickedCells);
        Task<List<TheatreSalon>> GetTheatreSalonsAsync();

        Task<TheatreSalon> GetTheatreSalonByIdAsync(Guid id);
        Task<List<SalonMovieTimelineViewModel>> GetSalonMovieTimelineViewModels(QueryObject query);
    }
}
