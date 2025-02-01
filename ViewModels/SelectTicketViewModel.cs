using System.ComponentModel.DataAnnotations;
using MyCinema.Attributes;
using MyCinema.Data;

namespace MyCinema.ViewModels
{
    public class SelectTicketViewModel
    {
        public string? PosterPath { get; set; }
        public string? Title { get;set; }
        public string? Language { get;set; }
        public List<string>? Genres { get; set; }
        public int SalonNumber { get; set; }
        public DateTime? ShowTime { get;set; }
        public decimal RegularTicketPrice { get; set; }
        public decimal VipTicketPrice { get; set; }
        public Guid ScreeningId { get; set; }
        [Range(0, 8,ErrorMessage = "Tickets can be maximum 8")]
        public int RegularTicketCount { get; set; }
        [Range(0, 8, ErrorMessage = "Tickets can be maximum 8")]
        public int VipTicketCount { get; set; }
        [Range(1, 8, ErrorMessage = "Total Tickets must be between 1 and 8")]
        public int TotalTickets => RegularTicketCount + VipTicketCount;
    }
}
