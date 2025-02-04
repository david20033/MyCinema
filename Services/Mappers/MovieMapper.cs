﻿using MyCinema.Data;
using MyCinema.Data.MovieApi;
using MyCinema.Data.MovieApi.Responses;
using MyCinema.Enums;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.Mappers.IMappers;
using MyCinema.ViewModels;

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
        public async Task<Movie> MapMovieDTOToEntity(MovieResponseDTO movieDTO, List<string> cast, List<string> crew)
        {
            var guidID = Guid.NewGuid();
            return new Movie
            {
                Id = guidID,
                moviedb_id = movieDTO.id ?? default,
                Adult = movieDTO.adult,
                Backdrop_path = movieDTO.backdrop_path,
                Belongs_to_collection_name = movieDTO.belongs_to_collection?.name ?? default,
                Budget = movieDTO.budget,
                Homapage = movieDTO.homapage,
                Imdb_id = movieDTO.imdb_id,
                Original_language = await _languageRepository.GetLanguageNameByIsoCodeAsync(movieDTO.original_language),
                Original_title = movieDTO.original_title,
                Overview = movieDTO.overview,
                Popularity = movieDTO.popularity,
                Poster_path = movieDTO.poster_path,
                Production_companies =movieDTO.production_companies?.Select(movieDTO => movieDTO.name).ToList() ?? new List<string?>(),
                Release_date = movieDTO.release_date,
                Revenue = movieDTO.revenue,
                Runtime = movieDTO.runtime,
                Spoken_languages = MapLanguageDTOsToEntities(movieDTO.spoken_languages),
                Status = StringToEnumMovieStatus(movieDTO.status),
                Tagline = movieDTO.tagline,
                Vote_avarage = movieDTO.vote_avarage,
                Vote_count = movieDTO.vote_count,
                Title = movieDTO.title,
                Genres = await MapMovieGenres(guidID, movieDTO.genres),
                Cast = cast,
                Crew = crew,
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
        public async Task<MovieDetailsViewModel> MapToMovieDetailViewModel(Movie Movie, MovieWithCreditsDTO MovieWithCreditsDTO)
        {
            var MovieDTO = MovieWithCreditsDTO?.Movie;
            var CreditsDTO = MovieWithCreditsDTO?.Credits;
            string lang = null;
            if (MovieDTO.original_language != null)
            {
                var result = await _languageRepository.GetLanguageNameByIsoCodeAsync(MovieDTO.original_language);
                lang = result.English_Name;
            }
            return new MovieDetailsViewModel
            {
                Id = Movie.Id,
                Adult = Movie.Adult ?? MovieDTO.adult,
                Backdrop_path = Movie.Backdrop_path ?? MovieDTO.backdrop_path,
                Budget = Movie.Budget ?? MovieDTO.budget,
                Homapage = Movie.Homapage ?? MovieDTO.homapage,
                Imdb_id = Movie.Imdb_id ?? MovieDTO.imdb_id,
                Original_language = Movie.Original_language?.English_Name ?? lang,
                Original_title = Movie.Original_title ?? MovieDTO.original_title,
                Overview = Movie.Overview ?? MovieDTO.overview,
                Popularity = Movie.Popularity ?? MovieDTO.popularity,
                Poster_path = Movie.Poster_path ?? MovieDTO.poster_path,
                Release_date = Movie.Release_date ?? MovieDTO.release_date,
                Revenue = Movie.Revenue ?? MovieDTO.revenue,
                RunTime = Movie.Runtime ?? MovieDTO.runtime,
                Spoken_languages =
                         Movie.Spoken_languages?.Select(m => m.English_Name).ToList()
                         ?? MovieDTO.spoken_languages?.Select(m => m.english_name).ToList(),
                Status = Movie.Status.ToString() ?? MovieDTO.status,
                Tagline = Movie.Tagline ?? MovieDTO.tagline,
                Title = Movie.Title ?? MovieDTO.title,
                Vote_average = Movie.Vote_avarage ?? MovieDTO.vote_avarage,
                Vote_count = (Movie.Vote_count != 0) ? Movie.Vote_count : (MovieDTO.vote_count != 0 ? MovieDTO.vote_count : 0),
                Genres = Movie.Genres?.Select(g => g.Genre.Name).ToList() ??
                         MovieDTO.genres?.Select(g => g.name).ToList(),
                Production_companies = Movie.Production_companies ?? MovieDTO.production_companies?.Select(p=>p.name).ToList(),
                Bellongs_to_collection = Movie.Belongs_to_collection_name ?? MovieDTO.belongs_to_collection?.name,
                Cast = Movie.Cast ?? CreditsDTO?.cast?.Select(p => p?.name)?.Take(10)?.ToList() ?? new List<string>(),
                Directors = Movie.Crew ?? CreditsDTO?.crew?.Where(m => m?.job == "Director")?.Select(p => p?.name)?.Take(5)?.ToList() ?? new List<string>(),
            };
        }
        public async Task<MovieListViewModel> MapToMovieListViewModel(Movie movie, MovieNowPlayingDTO movieDTO)
        {
            string lang=null;
            if (movieDTO?.original_language != null)
            {
                var result =await _languageRepository.GetLanguageNameByIsoCodeAsync(movieDTO.original_language);
                lang = result.English_Name;
            }
            return new MovieListViewModel
            {
                id = movie.Id != Guid.Empty ? movie.Id.ToString() : movieDTO.id != 0 ? movieDTO.id.ToString() : null,
                moviedbId = movie.moviedb_id ?? movieDTO.id,
                Title = movie.Title ?? movieDTO.title,
                Release_date = movie.Release_date ?? movieDTO.release_date,
                Original_language = movie.Original_language?.English_Name ?? lang,
                Adult = movie.Adult ?? movieDTO.adult,
                Vote_average = movie.Vote_avarage ?? (decimal?)movieDTO.vote_avarage,
                Vote_count = movie.Vote_count ?? movieDTO.vote_count
            };
        }
    }
}
