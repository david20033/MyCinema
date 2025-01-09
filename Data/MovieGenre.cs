using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCinema.Data
{
    public class MovieGenre
    {
        [Key]
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Guid GenreId { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }

    }
}
