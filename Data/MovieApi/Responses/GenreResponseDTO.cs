using Newtonsoft.Json;

namespace MyCinema.Data.MovieApi.Responses
{
    public class GenreResponseDTO
    {
        [JsonProperty("genres")]
        public List<GenreDTO>? genres { get; set; }
    }
}
