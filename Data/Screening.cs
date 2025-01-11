namespace MyCinema.Data
{
    public class Screening
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime.Add(Duration); 
        public TimeSpan Duration { get; set; }

        public Guid TheatreSalonId { get; set; }
        public TheatreSalon TheatreSalon { get; set; }

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        public decimal TicketPrice { get; set; }
        public List<string> ReservedSeats { get; set; } = new List<string>();

    }
}
