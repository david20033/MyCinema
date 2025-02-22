using MyCinema.Data;
using MyCinema.Helpers;

namespace MyCinema.Repositories.IRepositories
{
    public interface ISalonRepository
    {
        public Task AddSalonAsync(TheatreSalon salon);
        public Task<bool> IsSalonNumberExistsAsync(int salonNumber);
        public Task<List<TheatreSalon>> GetTheatreSalonsAsync();
        public Task<List<TheatreSalon>> GetTheatreSalonsWithScreeningsAsync(QueryObject query);

        public  Task<TheatreSalon> GetTheatreSalonByIdAsync(Guid id);
        Task<TimeSpan> GetCinemaOpenTimeAsync();
        Task<TimeSpan> GetCinemaCloseTimeAsync();
    }
}
