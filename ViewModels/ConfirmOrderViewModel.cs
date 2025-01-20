namespace MyCinema.ViewModels
{
    public class ConfirmOrderViewModel
    {
        public string? PosterPath { get; set; }
       public string? Title { get; set; }
        public DateTime ReleaseYear { get; set; }
        public DateTime ShowStartTime { get;set; }
        public DateTime ShowEndTime { get;set; }
        public string? Language { get; set; }
        public List<string> Genres { get; set; } = [];
        public int SalonNumber { get; set; }
        public decimal RegularTicketPrice { get;set; }
        public decimal VipTicketPrice { get; set; }
        public List<string> RegularTicketSeatsCoords { get; set; } = [];
        public List<string> VipTicketSeatsCoords { get; set; } = [];
        public Guid TicketOrderId { get; set; }
    }
}
