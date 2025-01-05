namespace MyCinema.Data
{
    public class TheatreSalon
    {
        public Guid Id { get; set; }
        public int SalonNumber { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int[] PlacesRows { get; set; }
        public int[] PlacesColumns { get; set; }
        public bool isVip {  get; set; } 
    }
}
