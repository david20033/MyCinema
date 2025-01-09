using MyCinema.Data;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
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
    }
}
