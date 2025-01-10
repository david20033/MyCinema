using MyCinema.Data;
using MyCinema.Data.MovieApi;
using MyCinema.Data.MovieApi.Responses;
using MyCinema.ViewModels;
using RestSharp;

namespace MyCinema.Services.IServices
{
    public interface IMovieService
    {
        Task<Movie> GetMovieWithPhotosByIdAsync(Guid id);
        Task<List<MovieNowPlayingDTO>> GetNowPlayingMoviesAsync(int page);
        Task AddMovieWithPhotosAsync(Movie movie, List<IFormFile> MoviePhotos);
        Task<AddMovieViewModel> GetAddMovieViewDataAsync();
        Task<List<Movie>> GetAllMoviesWithPhotosAsync();
        Task<MovieResponseDTO> GetMovieDetailsByIdFromAPI(int id);
        Task AddMovieRangeInDataBaseByIds(List<int> movies);
        Task<Movie> GetMovieDetailsByIdFromDb(Guid id);
        MovieDetailsViewModel MovieDetailsViewModel(Movie movie, MovieResponseDTO MovieDTO);
    }
}
