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
            var cinemaOpeningTime = TimeSpan.FromHours(9);
            var cinemaClosingTime = TimeSpan.FromHours(23);
            var totalDayDuration = cinemaClosingTime - cinemaOpeningTime;
            List<SalonMovieTimelineViewModel> result = [];
            foreach (var salon in salons) 
            {
                var screenings = salon.Screenings;
                var model = new SalonMovieTimelineViewModel
                {
                    SalonNumber = salon.SalonNumber
                };
                if(screenings == null || screenings.Count == 0)
                {
                    result.Add(model);
                }
                foreach (var screen in screenings) 
                {
                    if (query.Date != null)
                    {
                        if (screen.StartTime.Date != query.Date.Value.Date)
                        {
                            result.Add(model);
                            break;
                        }
                    }
                        model.MovieStartTime = screen.StartTime;
                        model.MovieEndTime = screen.EndTime;
                        model.Left = (int)Math.Floor(((screen.StartTime.TimeOfDay - cinemaOpeningTime).TotalMinutes/ totalDayDuration.TotalMinutes)*100);
                        model.Width =(int)Math.Floor((decimal)((screen.Movie.Runtime/totalDayDuration.TotalMinutes)*100));
                        model.MovieId = screen.MovieId;
                        model.SalonId = salon.Id;
                        model.Title = screen.Movie.Title;
                        model.SalonCapacity = salon.Capacity;
                        model.ReservedSeatsCount = screen.ReservedSeats.Count;
                    result.Add(model);
                }
            }
            return result;
        }
    }
}
