using MyCinema.Data;

namespace MyCinema.Repositories.IRepositories
{
    public interface ISalonRepository
    {
        public Task AddSalonAsync(TheatreSalon salon);
        public Task<bool> IsSalonNumberExistsAsync(int salonNumber);
    }
}
