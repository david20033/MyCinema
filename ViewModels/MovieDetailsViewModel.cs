using MyCinema.Data;

namespace MyCinema.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string? ApiId { get; set; }
        public bool? Adult { get; set; }
        public string? Backdrop_path { get; set; }
        public decimal? Budget { get; set; }
        public string? Homapage { get; set; }
        public string? Imdb_id { get; set; }
        public string? Original_language { get; set; }
        public string? Original_title { get; set; }
        public string? Overview { get; set; }
        public decimal? Popularity { get; set; }
        public string? Poster_path { get; set; }
        public string? Release_date { get; set; }
        public int? Revenue { get; set; }
        public int? RunTime { get; set; }
        public List<string>? Spoken_languages { get; set; }
        public string? Status { get; set; }
        public string? Tagline { get; set; }
        public string? Title { get; set; }
        public decimal? Vote_average { get; set; }
        public int? Vote_count { get; set; }
        public List<string>? Genres { get; set; }   
        
        public List<string?>? Production_companies { get; set; }
        public string? Bellongs_to_collection { get; set; }
        public List<string>? Cast { get; set; }
        public List<string>? Directors { get; set; }
        public List<Screening> Screenings { get; set; } = [];
    }
}
