using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi
{
    public class MovieCastDTO
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
        [JsonProperty("cast_id")]
        public int? cast_id { get; set; }
        [JsonProperty("character")]
        public string? character { get; set; }
        [JsonProperty("credit_id")]
        public string? credit_id { get; set; }
        [JsonProperty("order")]
        public int? order { get;set; }
    }
}
