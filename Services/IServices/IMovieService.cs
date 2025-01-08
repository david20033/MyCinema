using MyCinema.Data;
using MyCinema.Data.MovieApi;
using RestSharp;

namespace MyCinema.Services.IServices
{
    public interface IMovieService
    {
        Task<Movie> GetMovieWithPhotosByIdAsync(Guid id);
        Task<List<MovieNowPlayingDTO>> GetNowPlayingMoviesAsync(int page);
    }
}
