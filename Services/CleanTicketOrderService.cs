using MyCinema.Data;

namespace MyCinema.Services
{
    public class CleanTicketOrderService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(3);

        public CleanTicketOrderService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_interval, stoppingToken);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<MyCinemaDBContext>();

                    var rowsToDelete = dbContext.TicketOrder.Where(t => t.CustomerId == Guid.Empty);
                    dbContext.TicketOrder.RemoveRange(rowsToDelete);

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
