using MyCinema.Data;
using MyCinema.Data.MovieApi;
using MyCinema.Data.MovieApi.Responses;
using MyCinema.Helpers;
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
        Task<MovieWithCreditsDTO> GetMovieDetailsByIdFromAPI(int id);
        Task AddMovieRangeInDataBaseByIds(List<int> movies);
        Task<Movie> GetMovieDetailsByIdFromDb(Guid id);
        Task<MovieDetailsViewModel> MovieDetailsViewModel(Movie movie, MovieWithCreditsDTO MovieDTO);
        Task<List<Movie>> GetMoviesAsync(int page);
        Task<List<MovieListViewModel>> GetMovieListViewModelFromDb(int page);
        Task<List<MovieListViewModel>> GetMovieListViewModelFromApi(int page);
        Task<int> GetMoviesCount();
        Task<List<MovieScreeningViewModel>> GetMovieScreeningViewModelsAsync(QueryObject query);
        Task<List<MovieListViewModel>> GetMoviesFromApiBySearchTerm(string query);

    }
}
