using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi
{
    public class GenreDTO
    {
        [JsonProperty("id")]
        public int? id {  get; set; }
        [JsonProperty("name")]
        public string? name { get; set; }
    }
}
