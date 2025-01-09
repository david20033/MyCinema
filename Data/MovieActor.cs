using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyCinema.Data
{
    public class MovieActor
    {
        public Guid Id { get; set; }

        public Guid MovieId { get; set; }

        public Movie Movie { get; set; }

        public Guid ActorId { get; set; }

        public Actor Actor { get; set; }
    }
}
