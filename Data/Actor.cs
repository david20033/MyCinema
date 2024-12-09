namespace MyCinema.Data
{
    public class Actor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
