using Microsoft.EntityFrameworkCore;
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
            TimeSpan timeWindow = TimeSpan.FromHours(5);
            var screeningsInSalon = await _context.Screening
                .Where(s=> s.TheatreSalonId == screening.TheatreSalonId &&
                ((s.StartTime < screening.EndTime && s.StartTime >= screening.StartTime.Add(-timeWindow)) ||
                (s.EndTime > screening.StartTime && s.EndTime <= screening.EndTime.Add(timeWindow))))
                .ToListAsync();
            bool isOverlapping = screeningsInSalon
                   .Any(s => s.StartTime < screening.EndTime && s.EndTime > screening.StartTime);
            if (isOverlapping)
            {
                throw new InvalidOperationException("The screening time overlaps with an existing screening.");
            }

            await _context.Screening.AddAsync(screening);
            await _context.SaveChangesAsync();
        }
        public async Task<Screening> GetScreeningByIdAsync(Guid id)
        {
            return await _context.Screening
                .Where(s => s.Id == id)
                .Include(s=>s.Movie)
                .ThenInclude(s=>s.Original_language)
                .Include(s => s.Movie)
                .ThenInclude(s => s.Genres)
                .ThenInclude(s=>s.Genre)
                .Include(s=>s.TheatreSalon)
                .FirstOrDefaultAsync();
        }
    }
}
