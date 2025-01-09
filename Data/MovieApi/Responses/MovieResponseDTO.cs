using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi.Responses
{
    public class MovieResponseDTO
    {
        [JsonProperty("adult")]
        public bool? adult { get; set; }
        [JsonProperty("backdrop_path")]
        public string? backdrop_path { get; set; }
        [JsonProperty("belongs_to_collection")]
        public MovieCollectionDTO? belongs_to_collection { get; set; }
        [JsonProperty("budget")]
        public decimal? budget { get; set; }
        [JsonProperty("genres")]
        public List<GenreDTO>? genres { get; set; }
        [JsonProperty("homepage")]
        public string? homapage { get; set; }
        [JsonProperty("id")]
        public int? id { get; set; }
        [JsonProperty("imdb_id")]
        public string? imdb_id { get; set; }
        [JsonProperty("origin_country")]
        public List<string>? original_country { get; set; }
        [JsonProperty("original_language")]
        public string? original_language { get; set; }
        [JsonProperty("original_title")]
        public string? original_title { get; set; }
        [JsonProperty("overview")]
        public string? overview { get;set; }
        [JsonProperty("popularity")]
        public decimal? popularity { get; set; }
        [JsonProperty("poster_path")]
        public string? poster_path { get; set; }
        [JsonProperty("production_companies")]
        public List<ProductionCompanyDTO>? production_companies { get; set; }
        [JsonProperty("production_countries")]
        public List<CountryDTO>? production_countries { get; set; }
        [JsonProperty("release_date")]
        public string? release_date { get; set; }
        [JsonProperty("revenue")]
        public int? revenue { get; set; }
        [JsonProperty("runtime")]
        public int? runtime { get; set; }
        [JsonProperty("spoken_languages")]
        public List<LanguageDTO>? spoken_languages { get; set; }
        [JsonProperty("status")]
        public string? status { get; set; }
        [JsonProperty("tagline")]
        public string? tagline { get; set; }
        [JsonProperty("title")]
        public string? title { get; set; }
        [JsonProperty("video")]
        public bool? video { get; set; }
        [JsonProperty("vote_average")]
        public decimal? vote_avarage { get; set; }
        [JsonProperty("vote_count")]
        public int vote_count { get; set; }
    }
}
