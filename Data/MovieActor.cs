namespace MyCinema.Data
{
    public class MovieActor
    {
        public Guid Id { get; set; }
        public Actor Actor { get; set; }
        public Guid ActorId { get; set; }

        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }
    }
}
