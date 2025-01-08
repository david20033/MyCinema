using MyCinema.Data;
using MyCinema.Data.MovieApi;
using RestSharp;

namespace MyCinema.Services.IServices
{
    public interface IApiService
    {
        Task<List<MovieNowPlayingDTO>> GetNowPlayingMoviesAsync();
    }
}
