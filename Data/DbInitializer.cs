using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCinema.Services;
using MyCinema.Services.IServices;

namespace MyCinema.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(MyCinemaDBContext context, IApiService apiService, IMovieService movieService, ISalonService salonService, IAdminService adminService, IScreeningService screeningService, IServiceProvider serviceProvider, ITicketService ticketService)
        {
            await context.Database.MigrateAsync();
            //users
            if (context.Users.Count() == 1)
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                for(int i = 1; i <= 5; i++)
                {

                string username = $"user{i}@example.com";

                if (await userManager.FindByNameAsync(username) == null)
                {
                        string password = "123456a";
                    var user = new IdentityUser
                    {
                        UserName = username,
                        Email = username
                    };

                    var result = await userManager.CreateAsync(user, password);

                    if (!result.Succeeded)
                    {
                        throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
                }
                await context.SaveChangesAsync();
            }
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
                await context.SaveChangesAsync();
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
                await context.SaveChangesAsync();
            }
            //Screening
            if (!context.Screening.Any())
            {
                var salons = await salonService.GetTheatreSalonsAsync();
                for (int i = 0; i < salons.Count; i++)
                {
                    await SeedSalonWithScreenings(movieService, salonService, adminService,screeningService,i);
                }
                await context.SaveChangesAsync();
            }
            //Ticket, TicketOrder
            if (!context.TicketOrder.Any())
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var Screenings = await context.Screening.Include(s=>s.TheatreSalon).ToListAsync();
                var settings = await adminService.GetAppSettingsAsync();
                var Users = await userManager.Users.ToListAsync();
                var Price1 = settings.Where(k => k.Key == "RegularTicketPrice").FirstOrDefault()?.Value;
                var Price2 = settings.Where(k => k.Key == "VipTicketPrice").FirstOrDefault()?.Value;
                foreach (var screen in Screenings)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var randomUser = Users[RandomNumberGenerator.GetInt32(0, Users.Count)];
                        var ticketOrder = new TicketOrder
                        {
                            Id = Guid.NewGuid(),
                            OrderDate = DateTime.Now,
                        };
                        var Salon = screen.TheatreSalon;
                        await context.TicketOrder.AddAsync(ticketOrder);
                        int to = RandomNumberGenerator.GetInt32(1, 8);
                        for (int j = 0; j < to;  j++)
                        {
                            int randomType = RandomNumberGenerator.GetInt32(0, 2);
                            var seatCoords = GenerateSalonCoords(Salon);
                            while (Salon.EmptySeatsCoords.Contains(seatCoords))
                            {
                                seatCoords = GenerateSalonCoords(Salon);
                            }
                            decimal price = 0;
                            if(randomType == 1)
                            {
                                price = decimal.Parse(Price2);
                            }
                            else
                            {
                                price = decimal.Parse(Price1);
                            }
                            var ticket = new Ticket
                            {
                                Id = Guid.NewGuid(),
                                Price = price,
                                Type = (Enums.TicketType)randomType,
                                SeatNumber = seatCoords,
                                Screening = screen,
                                ScreeningId = screen.Id,
                                TicketOrder = ticketOrder,
                                TicketOrderId = ticketOrder.Id
                            };
                            screen.ReservedSeats.Add(seatCoords);
                            ticketOrder.Tickets.Add(ticket);
                            await context.Ticket.AddAsync(ticket);
                        }
                        await context.SaveChangesAsync();
                        await ticketService.ConfirmTicketOrder(ticketOrder.Id, randomUser.Id);
                    }
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
            var time = DateTime.Today.AddDays(1).AddHours(TimeSpan.Parse(cinemaOpenTime).Hours);
            var maxTime = DateTime.Today.AddDays(7).AddHours(TimeSpan.Parse(cinemaCloseTime).Hours);
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
                var to = DateTime.Today.AddDays(CurrentAddedDays).AddHours(TimeSpan.Parse(cinemaCloseTime).Hours); //debugging
                if (time >=to)
                {
                    if (time.Date != to.Date)
                    {
                        time = new DateTime(to.Year,to.Month,to.Day);
                    }
                    time=time.AddDays(1).Date;
                    time = time.AddHours(TimeSpan.Parse(cinemaOpenTime).Hours);
                    CurrentAddedDays++;
                }
            }
        }
        private static string GenerateSalonCoords(TheatreSalon salon)
        {
            int randomRow = RandomNumberGenerator.GetInt32(0, salon.Rows);
            int randomCol = RandomNumberGenerator.GetInt32(0, salon.Columns);
            return $"{randomRow},{randomCol}";
        }
    }
}
