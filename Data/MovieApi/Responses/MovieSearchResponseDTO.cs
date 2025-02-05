using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi.Responses
{
    public class MovieSearchResponseDTO
    {
        [JsonProperty("page")]
        public int page { get; set; }
        [JsonProperty("results")]
        public List<MovieNowPlayingDTO> results { get; set; } = [];
        [JsonProperty("total_pages")]
        public int total_pages { get;set; }
        [JsonProperty("total_results ")]
        public int total_results { get;set; }
    }
}
