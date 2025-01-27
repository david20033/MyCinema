using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface IAnalyticsService
    {
        Task<List<PaymentAnalyticsViewModel>> MapPaymentsForPeriodToPaymentAnalyticViewModel(DateTime StartDate, DateTime EndDate);
    }
}
