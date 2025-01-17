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
        public int RegularTicketCount { get; set; }
        public int VipTicketCount { get; set; }
    }
}
