using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi
{
    public class MovieCrewDTO
    {
        [JsonProperty("adult")]
        public bool? adult { get; set; }
        [JsonProperty("gender")]
        public int? gender { get; set; }
        [JsonProperty("id")]
        public int? id { get; set; }
        [JsonProperty("known_for_department")]
        public string? known_for_department { get; set; }
        [JsonProperty("name")]
        public string? name { get; set; }
        [JsonProperty("original_name")]
        public string? original_name { get; set; }
        [JsonProperty("popularity")]
        public decimal? popularity { get; set; }
        [JsonProperty("profile_path")]
        public string? profile_path { get; set; }
        [JsonProperty("credit_id")]
        public string? credit_id { get; set; }
        [JsonProperty("department")]
        public string? department { get; set; }
        [JsonProperty("job")]
        public string? job { get; set; }
    }
}
