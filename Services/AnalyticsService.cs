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
        public async Task<(List<PaymentAnalyticsViewModel> data, int totalCount)> GetPaymentAnalyticsViewModelsAsync(int pageNumber)
        {
            var (ticketOrders, totalCount) = await _analyticsRepository.GetAllTicketsOrdersWithDetailsForGivenPeriod(DateTime.MinValue, DateTime.MaxValue, pageNumber);

            var result = ticketOrders
                .Select(t => new PaymentAnalyticsViewModel
                {
                    TotalAmount = t.Tickets.Sum(ticket => ticket.Price),
                    VipTicketAmount = t.Tickets.Where(ticket => ticket.Type == Enums.TicketType.VIP).Sum(ticket => ticket.Price),
                    RegularTicketAmount = t.Tickets.Where(ticket => ticket.Type == Enums.TicketType.Regular).Sum(ticket => ticket.Price),
                    MovieTitle = t.Tickets.First().Screening.Movie.Title,
                    CustomerEmail = t.CustomerId.ToString(),
                    Date = t.OrderDate
                })
                .ToList();

            return (result, totalCount);
        }
    }
}
