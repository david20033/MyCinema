using MyCinema.Data;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
using MyCinema.Services.Mappers.IMappers;
using MyCinema.ViewModels;

namespace MyCinema.Services
{
    public class ScreeningService : IScreeningService
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly ISalonRepository _salonRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieMapper _movieMapper;
        public ScreeningService (IScreeningRepository screeningRepository, ISalonRepository salonRepository, IMovieRepository movieRepository, IMovieMapper movieMapper)
        {
            _screeningRepository = screeningRepository;
            _salonRepository = salonRepository;
            _movieRepository = movieRepository;
            _movieMapper = movieMapper;
        }
        public async Task<List<TheatreSalon>> GetSalonsAsync()
        {
            return await _salonRepository.GetTheatreSalonsAsync();
        }
        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }
        public async Task AddScreeningInDbAsync(AddScreeningViewModel model)
        {
            Movie movie = await _movieRepository.GetMovieDetailsByIdAsync(model.MovieId);
            var runtimeInMinutes = movie.Runtime;
            TimeSpan duration = TimeSpan.FromMinutes(runtimeInMinutes.Value);
            var screening = new Screening
            {
                Id = Guid.NewGuid(),
                StartTime = model.StartTime,
                TheatreSalonId = model.TheatreSalonId,
                MovieId = model.MovieId,
                TicketPrice = model.TicketPrice,
                Duration = duration,

            };
            await _screeningRepository.AddScreeningInDbAsync (screening);
        }
    }
}
