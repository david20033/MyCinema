using MyCinema.Data;

namespace MyCinema.ViewModels
{
    public class SelectTicketViewModel
    {
        //public Guid Id { get;set; }
        public string? PosterPath { get; set; }
        public string? Title { get;set; }
        public string? Language { get;set; }
        public List<string> Genres { get; set; }
        public int SalonNumber { get; set; }
        public DateTime? ShowTime { get;set; }
        public List<TicketTypeViewModel> TicketTypes { get; set; }

    }
}
