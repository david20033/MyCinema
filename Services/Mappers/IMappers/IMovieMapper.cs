using MyCinema.Data.MovieApi.Responses;
using MyCinema.Data;
using MyCinema.ViewModels;

namespace MyCinema.Services.Mappers.IMappers
{
    public interface IMovieMapper
    {
        Task<Movie> MapMovieDTOToEntity(MovieResponseDTO movieDTO);
        MovieDetailsViewModel MapToMovieDetailViewModel(Movie Movie, MovieResponseDTO MovieDTO);
    }
}
