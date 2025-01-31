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
            screening.EndTime = screening.StartTime.Add(screening.Duration);

            TimeSpan timeWindow = TimeSpan.FromHours(5);

            var screeningsInSalon = await _context.Screening
                .Where(s => s.TheatreSalonId == screening.TheatreSalonId)
                .ToListAsync();

            var overlappingScreenings = screeningsInSalon
                .Where(s => (s.StartTime < screening.EndTime && s.StartTime >= screening.StartTime.Add(-timeWindow)) ||
                            (s.EndTime > screening.StartTime && s.EndTime <= screening.EndTime.Add(timeWindow)))
                .ToList();

            bool isOverlapping = overlappingScreenings
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
        public async Task<List<Screening>> GetScreeningsByMovieAndDate(Guid movieId, DateTime date)
        {
            return await _context.Screening
                .Where(s => s.MovieId == movieId && s.StartTime.Date == date.Date)
                .ToListAsync();
        }
    }
}
