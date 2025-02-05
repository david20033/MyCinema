using MyCinema.Enums;

namespace MyCinema.Helpers
{
    public class TicketTypeHelper
    {
        public static Dictionary<TicketType, decimal> TicketPrices = new Dictionary<TicketType, decimal>
    {
        { TicketType.Regular, 12m },
        { TicketType.VIP, 20m }
    };
    }
}
