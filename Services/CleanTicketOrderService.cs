using MyCinema.Data;

namespace MyCinema.Services
{
    public class CleanTicketOrderService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(35);

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

                    var ticketsToDelete = dbContext.Ticket
                        .Where(t => t.TicketOrder.CustomerId == Guid.Empty)
                        .Select(t => new
                        {
                            t.Id,
                            t.ScreeningId,
                            t.SeatNumber
                        })
                        .ToList();

                    var ticketsGroupedByScreening = ticketsToDelete
                        .GroupBy(t => t.ScreeningId)
                        .ToDictionary(group => group.Key, group => group.Select(t => t.SeatNumber).ToList());

                    var ticketIdsToDelete = ticketsToDelete.Select(t => t.Id).ToList();
                    var ticketsToRemove = dbContext.Ticket.Where(t => ticketIdsToDelete.Contains(t.Id));
                    dbContext.Ticket.RemoveRange(ticketsToRemove);

                    foreach (var entry in ticketsGroupedByScreening)
                    {
                        var screeningId = entry.Key;
                        var seatsToRemove = entry.Value;
                        var screening = await dbContext.Screening.FindAsync(screeningId);
                        if (screening != null)
                        {
                            screening.ReservedSeats = screening.ReservedSeats
                                .Except(seatsToRemove)
                                .ToList();
                        }
                    }

                    await dbContext.SaveChangesAsync();
                }
            }
        }

    }
}
