using MyCinema.Data;
using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface ITicketService
    {
        Task AddTicketAsync(Ticket ticket);
        Task<SelectTicketViewModel> GetSelectTicketViewModel(Guid ScreeningId);
    }
}
