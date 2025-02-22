using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface IAnalyticsService
    {
        Task<AnalyticsIndexViewModel> MapPaymentsForPeriodToAnalyticsIndexViewModel(DateTime StartDate, DateTime EndDate);
        Task<List<AnalyticsMovieViewModel>> GetAnalyticsMovieViewModels(DateTime StartDate, DateTime EndDate);

        Task<(List<PaymentAnalyticsViewModel> data, int totalCount)> GetPaymentAnalyticsViewModelsAsync(int pageNumber);
    }
}
