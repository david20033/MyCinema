using MyCinema.Data;
using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface ITicketService
    {
        Task<SelectTicketViewModel> GetSelectTicketViewModel(Guid ScreeningId);
        Task AddTicketOwnershipInDbAsync(SelectTicketViewModel ticket);
    }
}
