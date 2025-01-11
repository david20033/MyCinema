using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi.Responses
{
    public class MovieCreditsResponseDTO
    {
        [JsonProperty("id")]
        public int? id { get;set; }
        [JsonProperty("cast")]
        public List<MovieCastDTO>? cast { get;set; }
        [JsonProperty("crew")]
        public List<MovieCrewDTO>? crew { get;set; }
    }
}
