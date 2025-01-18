using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Repositories.IRepositories;

namespace MyCinema.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly MyCinemaDBContext _context;
        public TicketRepository(MyCinemaDBContext context)
        {
            _context = context;
        }
        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Ticket.ToListAsync();
        }
        public async Task AddTicketOrderAsync(TicketOrder ticket)
        {
            await _context.TicketOrder.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        public async Task<TicketOrder> GetTicketOrderByIdAsync(Guid id)
        {
            return await _context.TicketOrder.Where(t => t.Id == id)
                .Include(t=>t.Tickets)
                .ThenInclude(t=>t.Screening)
                .ThenInclude(t=>t.TheatreSalon)
                .FirstOrDefaultAsync();
        }
    }
}
