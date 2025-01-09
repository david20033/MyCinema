using System.ComponentModel.DataAnnotations;

namespace MyCinema.Data
{
    public class Language
    {
        [Key]
        public Guid Id { get; set; }
        public string iso_code { get; set; }
        public string English_Name { get; set; }
        public string Name { get; set; }
        public ICollection<Movie>? SpokenMovies { get; set; }
    }
}
