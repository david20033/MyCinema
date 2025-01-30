using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Repositories.IRepositories;

namespace MyCinema.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly MyCinemaDBContext _context;
        public AnalyticsRepository(MyCinemaDBContext context)
        {
            _context = context;
        }
        public async Task<List<TicketOrder>> GetAllTicketsOrdersForGivenPeriod(DateTime startDate, DateTime endDate)
        {
            return await _context.TicketOrder
                .Where(t => (t.OrderDate >= startDate && t.OrderDate <= endDate) && t.CustomerId!=Guid.Empty)
                .Include(t=>t.Tickets)
                .OrderBy(t=>t.OrderDate)
                .ToListAsync();
        }
        public async Task<List<Movie>> GetMostProfitableMoviesForGivenPeriod(DateTime startDate, DateTime endDate, int limit)
        {
            return await _context.Movie
                .OrderBy(m => -m.Profit)
                .Take(limit)
                .ToListAsync();
        }
    }
}
