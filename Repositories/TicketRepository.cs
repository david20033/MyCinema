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
        public async Task AddTicketAsync(Ticket ticket)
        {
            await _context.Ticket.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
