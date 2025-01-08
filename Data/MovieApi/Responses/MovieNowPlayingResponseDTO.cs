using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MyCinema.Data.MovieApi.Responses
{
    public class MovieNowPlayingResponseDTO
    {
        [JsonProperty("dates")]
        public DatesDTO? dates { get; set; }
        [JsonProperty("page")]
        public int? page {  get; set; }
        [JsonProperty("results")]
        public List<MovieNowPlayingDTO> results { get; set; }

    }
    public class DatesDTO
    {
        [JsonProperty("minimum")]
        public string? minimum { get; set;}
        [JsonProperty("maximum")]
        public string? maximum { get; set; }
    }
}
