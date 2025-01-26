using MyCinema.Enums;

namespace MyCinema.Models
{
    public class TicketSummary
    {
        public TicketType Type { get; set; } 
        public int Quantity { get; set; } 
        public decimal PricePerTicket { get; set; }
    }
}
