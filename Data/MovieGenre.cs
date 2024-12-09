namespace MyCinema.Data
{
    public class MovieGenre
    {
        public Guid Id { get; set; }
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }

        public Genre Genre { get; set; }
        public Guid GenreId { get; set; }

    }
}
