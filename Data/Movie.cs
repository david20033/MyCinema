using MyCinema.Data.MovieApi;
using MyCinema.Enums;
using MyCinema.Migrations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCinema.Data
{
    public class Movie
    {
        [Required]
        public Guid Id { get; set; }
        public int moviedb_id { get; set; }

        public bool? Adult { get; set; }
        public string? Backdrop_path { get; set; }
        //public MovieCollectionDTO? Belongs_to_collection { get; set; }
        public decimal? Budget { get; set; }
        //public List<>? Actors { get; set; }
        public string? Homapage { get; set; }
        public string? Imdb_id { get; set; }
        // public List<string>? Original_country { get; set; }
        [ForeignKey("Original_languageId")]
        public Language? Original_language { get; set; }
        public Guid? Original_languageId { get; set; }
        public string? Original_title { get; set; }
        public string? Overview { get; set; }
        public decimal? Popularity { get; set; }
        public string? Poster_path { get; set; }
        //public List<ProductionCompanyDTO>? Production_companies { get; set; }
        //public List<CountryDTO>? production_countries { get; set; }
        public string? Release_date { get; set; }
        public int? Revenue { get; set; }
        public int? Runtime { get; set; }
        public ICollection<Language>? Spoken_languages { get; set; }
        public MovieStatus? Status { get; set; }
        public string? Tagline { get; set; }
        public string? Title { get; set; }
        public decimal? Vote_avarage { get; set; }
        public int Vote_count { get; set; }
        public ICollection<MoviePhoto>? MoviePhotos { get; set; }
        public ICollection<MovieGenre>? Genres { get; set; }
        public ICollection<MovieActor>? MovieActors { get; set; }
    }
}
