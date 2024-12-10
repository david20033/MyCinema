using System.ComponentModel.DataAnnotations;

namespace MyCinema.Data
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OriginalTitle { get; set; }
        public DateTime PremierDate { get; set; }
        [Range(10, 500)]
        public int DurationInMinutes { get; set; }
        public string Description { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public string MovieActors { get; set; }

        public ICollection<MoviePhoto> MoviePhotos { get; set; }    
        public bool Subtitles {  get; set; }    
    }
}
