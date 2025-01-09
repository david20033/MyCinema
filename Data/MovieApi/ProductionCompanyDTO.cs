using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi
{
    public class ProductionCompanyDTO
    {
        [JsonProperty("id")]
        public int? id { get; set; }
        [JsonProperty("logo_path")]
        public string? logo_path { get;set; }
        [JsonProperty("name")]
        public string? name { get; set; }
        [JsonProperty("origin_country")]
        public string? origin_country { get; set; }
    }
}
