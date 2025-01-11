using System.Security.Policy;

namespace MyCinema.ViewModels
{
    public class MovieListViewModel
    {
        public string? id { get; set; } 
        public int? moviedbId { get; set; }
        public string? Title { get; set; }
        public string? Release_date { get; set; }
        public string? Original_language { get; set; }
        public bool? Adult { get; set; }
        public decimal? Vote_average { get; set; }  
        public int? Vote_count { get; set; }
    }
}
