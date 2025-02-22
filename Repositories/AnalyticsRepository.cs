using Microsoft.AspNetCore.Identity;
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
                //.ThenInclude(t=>t.Screening)
                //.ThenInclude(t=>t.Movie)
                .OrderBy(t=>t.OrderDate)
                .ToListAsync();
        }
        public async Task<(List<TicketOrder> data, int totalCount)> GetAllTicketsOrdersWithDetailsForGivenPeriod(DateTime startDate, DateTime endDate, int pageNumber)
        {
            int pageSize = 10;
            var query = _context.TicketOrder
                .Where(t => t.OrderDate >= startDate && t.OrderDate <= endDate && t.CustomerId != Guid.Empty)
                .Include(t => t.Tickets)
                    .ThenInclude(t => t.Screening)
                    .ThenInclude(s => s.Movie)
                .OrderBy(t => t.OrderDate)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var data = await query
     .Skip((pageNumber - 1) * pageSize)
     .Take(pageSize)
     .Select(t => new TicketOrder
     {
         OrderDate = t.OrderDate,
         CustomerId = t.CustomerId,
         Tickets = t.Tickets.Select(ticket => new Ticket
         {
             Price = ticket.Price,
             Type = ticket.Type,
             Screening = new Screening
             {
                 Movie = new Movie
                 {
                     Title = ticket.Screening.Movie.Title
                 }
             }
         }).ToList() 
     })
     .ToListAsync();

            return (data, totalCount);
        }


        public async Task<List<Movie>> GetMostProfitableMoviesForGivenPeriod(DateTime startDate, DateTime endDate, int limit)
        {
            return await _context.Movie
                .OrderBy(m => -m.Profit)
                .Take(limit)
                .ToListAsync();
        }
        public async Task<string?> GetUserEmailById(string id)
        {
           var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            return user?.Email;
        }
    }
}
