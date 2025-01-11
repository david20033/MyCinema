using MyCinema.Data;
using MyCinema.Repositories.IRepositories;

namespace MyCinema.Repositories
{
    public class ScreeningRepository : IScreeningRepository
    {
        private readonly MyCinemaDBContext _context;
        public ScreeningRepository(MyCinemaDBContext context)
        {
            _context = context;
        }
        public async Task AddScreeningInDbAsync(Screening screening)
        {
            await _context.Screening.AddAsync(screening);
            await _context.SaveChangesAsync();
        }
    }
}
