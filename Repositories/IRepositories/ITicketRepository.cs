using MyCinema.Data;

namespace MyCinema.Repositories.IRepositories
{
    public interface ITicketRepository
    {
        Task AddTicketAsync(Ticket ticket);
        Task<List<Ticket>> GetAllTickets();
    }
}
