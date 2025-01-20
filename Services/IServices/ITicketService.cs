using MyCinema.Data;
using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface ITicketService
    {
        Task<SelectTicketViewModel> GetSelectTicketViewModel(Guid ScreeningId);
        Task<Guid> AddTicketOwnershipInDbAsync(SelectTicketViewModel ticket);
        Task<SelectSeatsViewModel> GetSelectSeatsViewModel(Guid id);
        Task SeedSeatsCoordsWithTicketOrder(SelectSeatsViewModel model);
        Task<ConfirmOrderViewModel> GetConfirmOrderViewModel(Guid id);
        Task AddUserIdInTickerOrder(Guid TicketOrderId, string UserId);
    }
}
