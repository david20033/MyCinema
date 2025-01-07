namespace MyCinema.Data
{
    public class TheatreSalon
    {
        public Guid Id { get; set; }
        public int SalonNumber { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public List<string> EmptySeatsCoords { get; set; } = new List<string>();
        public int Capacity { get; set; }
        public bool isVip { get; set; }

    }
}