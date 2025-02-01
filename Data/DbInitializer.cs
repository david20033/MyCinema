using Microsoft.EntityFrameworkCore;
using MyCinema.Services;
using MyCinema.Services.IServices;

namespace MyCinema.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(MyCinemaDBContext context, IApiService apiService, IMovieService movieService)
        {
            await context.Database.MigrateAsync();
            //Genres
            if (!context.Genre.Any())
            {
                var Genres = await apiService.GetGenresAsync();

                var GenresList = Genres.Select(dto => new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = dto.name
                }).ToList();
                await context.Genre.AddRangeAsync(GenresList);
                await context.SaveChangesAsync();
            }
            //Languages
            if (!context.Language.Any())
            {
                var Languages = await apiService.GetLanguagesAsync();

                var LanguageList = Languages.Select(dto => new Language
                {
                    Id = Guid.NewGuid(),
                    iso_code = dto.iso_639_1,
                    English_Name = dto.english_name,
                    Name = dto.name
                }).ToList();

                await context.Language.AddRangeAsync(LanguageList);
                await context.SaveChangesAsync();
            }
            //Movies
            if (!context.Movie.Any())
            {
                var movies = await apiService.GetNowPlayingMoviesAsync(1);
                var movieIds = new List<int>();
                movies.ForEach(t => movieIds.Add(t.id ?? 0));
                await movieService.AddMovieRangeInDataBaseByIds(movieIds);
            }


            await context.SaveChangesAsync();
        }
    }
}
