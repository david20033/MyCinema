namespace MyCinema.ViewModels
{
    public class AnalyticsMovieViewModel
    {
        public Guid movieId { get; set; }
        public string? Title { get; set; }
        public string? Poster_Path { get; set; }

        public decimal Profit { get; set; }
        public int TicketSoldCount { get; set; }
    }
}
