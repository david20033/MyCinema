using MyCinema.Data;
using MyCinema.Models;
using MyCinema.ViewModels;
using Stripe.Checkout;

namespace MyCinema.Services.IServices
{
    public interface ITicketService
    {
        Task<SelectTicketViewModel> GetSelectTicketViewModel(Guid ScreeningId);
        Task<Guid> AddTicketOwnershipInDbAsync(SelectTicketViewModel ticket);
        Task<SelectSeatsViewModel> GetSelectSeatsViewModel(Guid id);
        Task SeedSeatsCoordsWithTicketOrder(SelectSeatsViewModel model);
        Task UnSeedSeatsCoordsWithTicketOrder(Guid TicketOrderId);
        Task<ConfirmOrderViewModel> GetConfirmOrderViewModel(Guid id);
        Task AddUserIdInTickerOrder(Guid TicketOrderId, string UserId);
        Task<Session> CreateStripeSession(Guid id);
        Task<bool> IsTicketOrderExists(Guid Id);
    }
}
