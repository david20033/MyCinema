using Microsoft.AspNetCore.Identity;
using MyCinema.Enums;

namespace MyCinema.Data
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public TicketType Type { get; set; }
        public decimal Price { get; set; }

    }
}
