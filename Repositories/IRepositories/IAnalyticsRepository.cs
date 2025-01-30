using MyCinema.Data;

namespace MyCinema.Repositories.IRepositories
{
    public interface IAnalyticsRepository
    {
        Task<List<TicketOrder>> GetAllTicketsOrdersForGivenPeriod(DateTime startDate, DateTime endDate);
        Task<List<Movie>> GetTopThreeMostProfitableMoviesForGivenPeriod(DateTime startDate, DateTime endDate, int limit);
    }
}
