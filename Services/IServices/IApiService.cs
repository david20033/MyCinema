using MyCinema.Data;
using MyCinema.Data.MovieApi;
using MyCinema.Data.MovieApi.Responses;
using RestSharp;

namespace MyCinema.Services.IServices
{
    public interface IApiService
    {
        Task<List<MovieNowPlayingDTO>> GetNowPlayingMoviesAsync();
        Task<List<LanguageDTO>> GetLanguagesAsync();
    }
}
