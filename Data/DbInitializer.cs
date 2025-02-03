using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using MyCinema.Services;
using MyCinema.Services.IServices;

namespace MyCinema.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(MyCinemaDBContext context, IApiService apiService, IMovieService movieService, ISalonService salonService, IAdminService adminService, IScreeningService screeningService)
        {
            await context.Database.MigrateAsync();
            if (!context.AppSetting.Any())
            {
                context.AppSetting.AddRange(
                    new AppSetting { Key = "RegularTicketPrice", Value = "13", Description="Regular Ticket Price", InputType = Enums.InputType.number },
                    new AppSetting { Key = "VipTicketPrice", Value = "17" ,Description = "VIP Ticket Price", InputType = Enums.InputType.number },
                    new AppSetting { Key = "CinemaOpenHour", Value = "8", Description = "Cinema Open Hour",InputType=Enums.InputType.time},
                    new AppSetting { Key = "CinemaCloseHour", Value = "17", Description = "Cinema Close Hour", InputType = Enums.InputType.time }
                    );
                await context.SaveChangesAsync();
            }
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
            //Movies, MovieGenre
            if (!context.Movie.Any())
            {
                var movies = await apiService.GetNowPlayingMoviesAsync(1);
                var movieIds = new List<int>();
                movies.ForEach(t => movieIds.Add(t.id ?? 0));
                await movieService.AddMovieRangeInDataBaseByIds(movieIds);
            }
            //TheatreSalon
            if (!context.TheatreSalon.Any())
            {
                await   salonService.AddSalonAsync
                    (
                    new TheatreSalon
                    {
                        Id = Guid.NewGuid(),
                        SalonNumber = 1,
                        Rows = 6,
                        Columns = 15,
                        isVip = true,
                    },
                    //"0,5","1,5","2,5","2,6","2,7","2,8","2,9","1,9","0,9","0,8","0,7","0,6","1,6","1,7","1,8"
                    "[\"0,5\",\"1,5\",\"2,5\",\"2,6\",\"2,7\",\"2,8\",\"2,9\",\"1,9\",\"0,9\",\"0,8\",\"0,7\",\"0,6\",\"1,6\",\"1,7\",\"1,8\"]"
                    );
                await salonService.AddSalonAsync
                    (
                    new TheatreSalon
                    {
                        Id = Guid.NewGuid(),
                        SalonNumber = 2,
                        Rows = 6,
                        Columns = 14,
                        isVip = true,
                    },
                    ""
                    );
            }
            //Screening
            if (!context.Screening.Any())
            {
                var salons = await salonService.GetTheatreSalonsAsync();
                for (int i = 0; i < salons.Count; i++)
                {
                    await SeedSalonWithScreenings(movieService, salonService, adminService,screeningService,i);
                }
            }

            await context.SaveChangesAsync();
        }
        private static async Task SeedSalonWithScreenings( IMovieService movieService, ISalonService salonService, IAdminService adminService, IScreeningService screeningService, int SalonIndex)
        {
            var movies = await movieService.GetMoviesAsync(1);
            var salons = await salonService.GetTheatreSalonsAsync();
            var settings = await adminService.GetAppSettingsAsync();
            var cinemaOpenTime = settings.Where(k => k.Key == "CinemaOpenHour").FirstOrDefault()?.Value;
            var cinemaCloseTime = settings.Where(k => k.Key == "CinemaCloseHour").FirstOrDefault()?.Value;
            int CurrentAddedDays = 1;
            var time = DateTime.Today.AddDays(1).AddHours(double.Parse(cinemaOpenTime));
            var maxTime = DateTime.Today.AddDays(7).AddHours(double.Parse(cinemaCloseTime));
            while (time <= maxTime)
            {
                int index = RandomNumberGenerator.GetInt32(0, movies.Count);
                var movieDuration = TimeSpan.FromMinutes(movies[index].Runtime.Value);
                await screeningService.AddScreeningInDbAsync
                    (
                    new ViewModels.AddScreeningViewModel
                    {
                        StartTime = time,
                        Duration = movieDuration,
                        TheatreSalonId = salons[SalonIndex].Id,
                        MovieId = movies[index].Id,
                        TicketPrice = 12,
                    }
                    );
                time = time.Add(movieDuration).AddMinutes(30);
                if (time >= DateTime.Today.AddDays(CurrentAddedDays).AddHours(double.Parse(cinemaCloseTime)))
                {
                    time=time.AddDays(1).Date;
                    time = time.AddHours(double.Parse(cinemaOpenTime));
                    CurrentAddedDays++;
                }
            }
        }
    }
}
