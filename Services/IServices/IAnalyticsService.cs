using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface IAnalyticsService
    {
        Task<AnalyticsIndexViewModel> MapPaymentsForPeriodToPaymentAnalyticViewModel(DateTime StartDate, DateTime EndDate);
    }
}
