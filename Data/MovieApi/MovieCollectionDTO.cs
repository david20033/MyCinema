using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi
{
    public class MovieCollectionDTO
    {
        [JsonProperty("id")]
        public int? id { get; set; }
        [JsonProperty("name")]
        public string? name { get; set; }
        [JsonProperty("poster_path")]
        public string? poster_path { get; set; }
        [JsonProperty("backdrop_path")]
        public string? backdrop_path { get; set; }
    }
}
