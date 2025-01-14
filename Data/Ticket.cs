using Microsoft.AspNetCore.Identity;
using MyCinema.Enums;

namespace MyCinema.Data
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public TicketType Type { get; set; }
        public decimal Price { get; set; }
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }
        public DateTime ShowDateTime { get; set; }
        public string? SeatNumber { get; set; }

    }
}
