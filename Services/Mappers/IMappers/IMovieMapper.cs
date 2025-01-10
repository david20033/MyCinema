using MyCinema.Data.MovieApi.Responses;
using MyCinema.Data;

namespace MyCinema.Services.Mappers.IMappers
{
    public interface IMovieMapper
    {
        Task<Movie> MapMovieDTOToEntity(MovieResponseDTO movieDTO);
    }
}
