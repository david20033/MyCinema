using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;

namespace MyCinema.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        public AnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }
        public async Task<List<PaymentAnalyticsViewModel>> MapPaymentsForPeriodToPaymentAnalyticViewModel(DateTime StartDate, DateTime EndDate)
        {
           var data = await _analyticsRepository.GetAllTicketsOrdersForGivenPeriod(StartDate, EndDate);
            return data.GroupBy(t => t.OrderDate.Date)
                .Select(g => new PaymentAnalyticsViewModel
                {
                    Date = g.Key,
                    TotalAmount = (int)g.Sum(t => t.Tickets.Select(t => t.Price).Sum())
                }).ToList();
        }
    }
}
