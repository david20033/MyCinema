using Microsoft.AspNetCore.Identity;
using MyCinema.Data;
using MyCinema.ViewModels;

namespace MyCinema.Repositories.IRepositories
{
    public interface IAnalyticsRepository
    {
        Task<List<TicketOrder>> GetAllTicketsOrdersForGivenPeriod(DateTime startDate, DateTime endDate);
        Task<(List<TicketOrder> data, int totalCount)> GetAllTicketsOrdersWithDetailsForGivenPeriod(DateTime startDate, DateTime endDate, int pageNumber);
        Task<List<Movie>> GetMostProfitableMoviesForGivenPeriod(DateTime startDate, DateTime endDate, int limit);
        Task<string?> GetUserEmailById(string id);
    }
}
