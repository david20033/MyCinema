namespace MyCinema.Data
{
    public class TheatreSalon
    {
        public Guid Id { get; set; }
        public int SalonNumber { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public bool[] PlacesRows { get; set; }
        public bool[] PlacesColumns { get; set; }
        public bool isVip { get; set; }

        public TheatreSalon()
        {
            PlacesRows = new bool[0]; 
            PlacesColumns = new bool[0]; 
        }

        public void InitializePlaces()
        {
            PlacesRows = new bool[Rows];
            PlacesColumns = new bool[Columns];
        }
    }
}