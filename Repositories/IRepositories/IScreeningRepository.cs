using MyCinema.Data;

namespace MyCinema.Repositories.IRepositories
{
    public interface IScreeningRepository
    {
        Task AddScreeningInDbAsync(Screening screening);
        Task<Screening> GetScreeningByIdAsync(Guid id);
    }
}
