namespace MyCinema.Data.MovieApi.Responses
{
    public class MovieWithCreditsDTO
    {
        public MovieCreditsResponseDTO? Credits { get; set; }
        public MovieResponseDTO? Movie { get; set; }
    }
}
