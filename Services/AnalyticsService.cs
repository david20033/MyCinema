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
        public async Task<AnalyticsIndexViewModel> MapPaymentsForPeriodToPaymentAnalyticViewModel(DateTime StartDate, DateTime EndDate)
        {
           var TicketOrder = await _analyticsRepository.GetAllTicketsOrdersForGivenPeriod(StartDate, EndDate);
            var TopMovies = await _analyticsRepository.GetTopThreeMostProfitableMoviesForGivenPeriod(StartDate, EndDate, 3);
            var AnalyticsList = TicketOrder.GroupBy(t => t.OrderDate.Date)
                .Select(g => new PaymentAnalyticsViewModel
                {
                    Date = g.Key,
                    TotalAmount = (int)g.Sum(t => t.Tickets.Select(t => t.Price).Sum())
                }).ToList();
            var MoviesList = TopMovies
                .Select(m => new AnalyticsMovieViewModel
                {
                    movieId = m.Id,
                    Title = m.Title,
                    Poster_Path = m.Poster_path,
                    Profit = m.Profit,
                }).ToList();
            return new AnalyticsIndexViewModel
            {
                Analytics = AnalyticsList,
                Movies = MoviesList,
            };
        }
    }
}
