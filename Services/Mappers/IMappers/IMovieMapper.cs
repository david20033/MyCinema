using MyCinema.Data.MovieApi.Responses;
using MyCinema.Data;
using MyCinema.ViewModels;
using MyCinema.Data.MovieApi;

namespace MyCinema.Services.Mappers.IMappers
{
    public interface IMovieMapper
    {
        Task<Movie> MapMovieDTOToEntity(MovieResponseDTO movieDTO, List<string> cast, List<string> crew);
        MovieDetailsViewModel MapToMovieDetailViewModel(Movie Movie, MovieWithCreditsDTO MovieDTO);
        Task<MovieListViewModel> MapToMovieListViewModel(Movie movie, MovieNowPlayingDTO movieDTO);
    }
}
