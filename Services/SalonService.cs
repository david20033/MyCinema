using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyCinema.Data;
using MyCinema.Helpers;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;
using System.Text.Json;

namespace MyCinema.Services
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _salonRepository;

        public SalonService(ISalonRepository salonRepository)
        {
            _salonRepository = salonRepository;
        }
        public async Task AddSalonAsync(TheatreSalon salon, string clickedCells)
        {
            if (await _salonRepository.IsSalonNumberExistsAsync(salon.SalonNumber))
            {
                throw new ArgumentException("The salon number already exists. Please choose a different number.");
            }
            if (!string.IsNullOrEmpty(clickedCells))
            {
                var clickedCoordinates = JsonSerializer.Deserialize<List<string>>(clickedCells);
                foreach (var coordinate in clickedCoordinates)
                {
                    salon.EmptySeatsCoords.Add(coordinate);
                }
            }
            salon.Id = Guid.NewGuid();
            salon.Capacity = salon.Rows * salon.Columns - salon.EmptySeatsCoords.Count;
            await _salonRepository.AddSalonAsync(salon);
        }
        public async Task<List<TheatreSalon>> GetTheatreSalonsAsync()
        {
            return await _salonRepository.GetTheatreSalonsAsync();
        }
        public async Task<TheatreSalon> GetTheatreSalonByIdAsync(Guid id)
        {
            return await _salonRepository.GetTheatreSalonByIdAsync(id);
        }
        public async Task<List<SalonMovieTimelineViewModel>> GetSalonMovieTimelineViewModels(QueryObject query)
        {
            var salons = await _salonRepository.GetTheatreSalonsAsync();
            var cinemaOpeningTime = await _salonRepository.GetCinemaOpenTimeAsync();
            var cinemaClosingTime = await _salonRepository.GetCinemaCloseTimeAsync();
            var totalDayDuration = cinemaClosingTime - cinemaOpeningTime;
            List<SalonMovieTimelineViewModel> result = [];
            DateTime date;
            if (query.Date != null)
            {
                date = query.Date.Value.Date;
            }
            else
            {
                date = DateTime.Now.Date;
            }
            foreach (var salon in salons) 
            {
                var screenings = salon.Screenings.Where(s => s.StartTime.Date == date).ToList();
                int salonNumber = salon.SalonNumber;
                if (screenings == null || screenings.Count == 0)
                {

                    result.Add(new SalonMovieTimelineViewModel
                    {
                        SalonNumber = salonNumber,
                        CinemaOpenTime = cinemaOpeningTime,
                        CinemaCloseTime = cinemaClosingTime,
                    });
                }
                foreach (var screen in screenings) 
                {
                    result.Add(new SalonMovieTimelineViewModel
                    {
                        SalonNumber=salonNumber,
                        MovieStartTime = screen.StartTime,
                        MovieEndTime = screen.EndTime,
                        Left = (int)Math.Floor(((screen.StartTime.TimeOfDay - cinemaOpeningTime).TotalMinutes / totalDayDuration.TotalMinutes) * 100),
                        Width = (int)Math.Floor((decimal)((screen.Movie.Runtime / totalDayDuration.TotalMinutes) * 100)),
                        MovieId = screen.MovieId,
                        SalonId = salon.Id,
                        Title = screen.Movie.Title,
                        SalonCapacity = salon.Capacity,
                        ReservedSeatsCount = screen.ReservedSeats.Count,
                        CinemaOpenTime = cinemaOpeningTime,
                        CinemaCloseTime =cinemaClosingTime,
                    });
                }
            }
            return result;
        }
    }
}
