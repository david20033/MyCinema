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
        public async Task<AnalyticsIndexViewModel> MapPaymentsForPeriodToAnalyticsIndexViewModel(DateTime StartDate, DateTime EndDate)
        {
           var TicketOrder = await _analyticsRepository.GetAllTicketsOrdersForGivenPeriod(StartDate, EndDate);
            var TopMovies = await _analyticsRepository.GetMostProfitableMoviesForGivenPeriod(StartDate, EndDate, 3);
            var AnalyticsList = TicketOrder.GroupBy(t => t.OrderDate.Date)
                .Select(g => new PaymentAnalyticsViewModel
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(t => t.Tickets.Select(t => t.Price).Sum())
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
        public async Task<List<AnalyticsMovieViewModel>> GetAnalyticsMovieViewModels(DateTime StartDate, DateTime EndDate)
        {
            var MoviesList = await _analyticsRepository.GetMostProfitableMoviesForGivenPeriod(StartDate, EndDate,10);
            var result = new List<AnalyticsMovieViewModel>();
            foreach(var movie in MoviesList)
            {
                result.Add(new AnalyticsMovieViewModel
                {
                    movieId = movie.Id,
                    Poster_Path = movie.Poster_path,
                    Profit = movie.Profit,
                    Title = movie.Title,
                    TicketSoldCount = movie.TicketSoldCount,
                });
            }
            return result;
        }
        public async Task<List<PaymentAnalyticsViewModel>> GetPaymentAnalyticsViewModelsAsync()
        {
            var TicketOrder = await _analyticsRepository.GetAllTicketsOrdersForGivenPeriod(DateTime.MinValue, DateTime.MaxValue);
            var result = TicketOrder
                .Select(t => new PaymentAnalyticsViewModel
                {
                    TotalAmount = t.Tickets.Select(t=>t.Price).Sum(),
                    VipTicketAmount = t.Tickets.Where(t=>t.Type==Enums.TicketType.VIP).Select(p=>p.Price).Sum(),
                    RegularTicketAmount = t.Tickets.Where(t => t.Type == Enums.TicketType.Regular).Select(p => p.Price).Sum(),
                    MovieTitle = t.Tickets[0].Screening.Movie.Title,
                    CustomerEmail = t.CustomerId.ToString(),
                    Date=t.OrderDate,

                }).ToList();
            return result;
        }
    }
}
