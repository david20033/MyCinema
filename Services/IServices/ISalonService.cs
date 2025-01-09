using MyCinema.Data;

namespace MyCinema.Services.IServices
{
    public interface ISalonService
    {
        Task AddSalonAsync(TheatreSalon salon, string clickedCells);
        Task<List<TheatreSalon>> GetTheatreSalonsAsync();

        Task<TheatreSalon> GetTheatreSalonByIdAsync(Guid id);
    }
}
