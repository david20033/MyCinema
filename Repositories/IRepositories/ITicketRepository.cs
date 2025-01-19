using MyCinema.Data;

namespace MyCinema.Repositories.IRepositories
{
    public interface ITicketRepository
    {
        Task AddTicketOrderAsync(TicketOrder ticket);
        Task<List<Ticket>> GetAllTickets();
        Task<TicketOrder> GetTicketOrderByIdAsync(Guid id);
        Task SaveAsync();
    }
}
