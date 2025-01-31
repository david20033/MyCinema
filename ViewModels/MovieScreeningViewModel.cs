using MyCinema.Data;

namespace MyCinema.ViewModels
{
    public class MovieScreeningViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public List<MovieGenre>? Genres { get; set; }
        public int? RunTime { get; set; }
        public string? Poster_path { get; set; }
        public bool? isAdult { get; set; }
        public List<Screening> Screenings { get; set; } = [];
        
    }
}
