using MyCinema.Enums;

namespace MyCinema.Data
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string SeatNumber { get; set; }
        public TicketType Type { get; set; } 
        public Guid ScreeningId { get; set; }
        public Screening Screening { get; set; }
        public Guid TicketOrderId { get; set; } 
        public TicketOrder TicketOrder { get; set; }
    }
}
