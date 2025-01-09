using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi
{
    public class CountryDTO
    {
        [JsonProperty("iso_3166_1")]
        public string? iso_3166_1 { get; set; }
        [JsonProperty("name")]
        public string? name { get;set; }
    }
}
