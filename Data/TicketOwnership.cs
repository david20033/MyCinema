using Microsoft.AspNetCore.Identity;

namespace MyCinema.Data
{
    public class TicketOwnership
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; } 
        public Guid TicketId { get; set; }
        public Guid TheatreSalonId { get; set; }
        public Guid MovieId { get; set; }


        public IdentityUser? User { get; set; }
        public List<Ticket>? Tickets { get; set; }
        public TheatreSalon? Salon { get; set; }
        public Movie? Movie { get; set; }
        public List<string>? SeatsCoords { get; set; }
    }
}
