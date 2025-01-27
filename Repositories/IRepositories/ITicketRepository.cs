using MyCinema.Data;
using MyCinema.Models;

namespace MyCinema.Repositories.IRepositories
{
    public interface ITicketRepository
    {
        Task AddTicketOrderAsync(TicketOrder ticket);
        Task<List<Ticket>> GetAllTickets();
        Task<TicketOrder> GetTicketOrderByIdAsync(Guid id);
        Task SaveAsync();
        Task<List<TicketSummary>> GetTicketSummaryByTicketOrderId(Guid id);
        Task<bool> IsTicketOrderExist(Guid id);
    }
}
