using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi
{
    public class LanguageDTO
    {
        [JsonProperty("iso_639_1")]
        public string iso_639_1 { get; set; }
        [JsonProperty("english_name")]
        public string english_name { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
    }
}
