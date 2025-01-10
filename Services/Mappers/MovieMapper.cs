using MyCinema.Data;
using MyCinema.Data.MovieApi;
using MyCinema.Data.MovieApi.Responses;
using MyCinema.Enums;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.Mappers.IMappers;

namespace MyCinema.Services.Mappers
{
    public class MovieMapper : IMovieMapper
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IGenreRepository _genreRepository;
        public MovieMapper(ILanguageRepository languageRepository, IGenreRepository genreRepository)
        {
            _languageRepository = languageRepository;
            _genreRepository = genreRepository;
        }
        public async Task<Movie> MapMovieDTOToEntity(MovieResponseDTO movieDTO)
        {
            var guidID = Guid.NewGuid();
            return new Movie
            {
                Id = guidID,
                moviedb_id = movieDTO.id ?? default,
                Adult = movieDTO.adult,
                Backdrop_path = movieDTO.backdrop_path,
                Budget = movieDTO.budget,
                Homapage = movieDTO.homapage,
                Imdb_id = movieDTO.imdb_id,
                Original_language = await _languageRepository.GetLanguageNameByIsoCodeAsync(movieDTO.original_language),
                Original_title = movieDTO.original_title,
                Overview = movieDTO.overview,
                Popularity = movieDTO.popularity,
                Poster_path = movieDTO.poster_path,
                Release_date = movieDTO.release_date,
                Revenue = movieDTO.revenue,
                Runtime = movieDTO.runtime,
                Spoken_languages = MapLanguageDTOsToEntities(movieDTO.spoken_languages),
                Status = StringToEnumMovieStatus(movieDTO.status),
                Tagline = movieDTO.tagline,
                Vote_avarage = movieDTO.vote_avarage,
                Vote_count = movieDTO.vote_count,
                Title = movieDTO.title,
                Genres = await MapMovieGenres(guidID, movieDTO.genres)
            };
        }
        private List<Language>? MapLanguageDTOsToEntities(List<LanguageDTO> langsDTO)
        {
            return langsDTO?.Select(lang => new Language
            {
                Id = Guid.NewGuid(),
                iso_code = lang.iso_639_1,
                English_Name = lang.english_name,
                Name = lang.english_name,
            }).ToList();
        }

        private MovieStatus StringToEnumMovieStatus(string status)
        {
            return Enum.TryParse<MovieStatus>(status, true, out var parsedStatus)
                ? parsedStatus
                : MovieStatus.Unknown;
        }
        private async Task<List<Genre>> MapGenreDTOToEntityCollection (List<GenreDTO> genreDTO)
        {
            var result = new List<Genre>();
            foreach(var g in genreDTO)
            {
                var genre = await _genreRepository.GetGenreByName(g.name);
                result.Add(new Genre
                {
                    Id = genre.Id,
                    Name = genre.Name,
                });
            }
            return result;
        }
        private async Task<List<MovieGenre>> MapMovieGenres(Guid MovieId, List<GenreDTO> genreDTO)
        {
            List<Genre> Genres = await MapGenreDTOToEntityCollection(genreDTO);
            var result = new List<MovieGenre>();
            foreach (var genre in Genres)
            {
                result.Add(new MovieGenre
                {
                    Id = Guid.NewGuid(),
                    MovieId = MovieId,
                    GenreId = genre.Id,
                });
            }
            return result;
        }
    }
}
