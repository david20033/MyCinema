using Microsoft.AspNetCore.Identity;

namespace MyCinema.Data
{
    public class TicketOwnership
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } 
        public Guid TicketId { get; set; } 

        public IdentityUser User { get; set; }
        public Ticket Ticket { get; set; }
    }
}
