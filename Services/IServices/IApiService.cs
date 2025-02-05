using MyCinema.Data;
using MyCinema.Data.MovieApi;
using MyCinema.Data.MovieApi.Responses;
using RestSharp;

namespace MyCinema.Services.IServices
{
    public interface IApiService
    {
        Task<List<MovieNowPlayingDTO>> GetNowPlayingMoviesAsync(int page);
        Task<List<LanguageDTO>> GetLanguagesAsync();
        Task<List<GenreDTO>> GetGenresAsync();
        Task<MovieResponseDTO> GetMovieDetailsByIdAsync(int id);
        Task<MovieCreditsResponseDTO> GetMovieCreditsByIdAsync(int id);
        Task<MovieWithCreditsDTO> GetMovieWithCreditsByIdDTO(int id);
        Task<MovieSearchResponseDTO> GetMovieSearchResponseAsync(string query);
    }
}
