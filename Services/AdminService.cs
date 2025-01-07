using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Enums;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;

namespace MyCinema.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ISalonRepository _salonRepository;
        private readonly MyCinemaDBContext _context;
        private readonly EnumServices _enumServices;
        public AdminService (IMovieRepository movieRepository, ISalonRepository salonRepository, MyCinemaDBContext context, EnumServices enumServices)
        {
            _movieRepository = movieRepository;
            _salonRepository = salonRepository;
            _context = context;
            _enumServices = enumServices;
        }
        public async Task AddMovieWithPhotosAsync(Movie movie, List<IFormFile> MoviePhotos)
        {
            await _movieRepository.AddMovieAsync(movie);
            if (MoviePhotos != null && MoviePhotos.Any())
            {
                foreach (var file in MoviePhotos)
                {
                    if (file.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);

                        var MoviePhoto = new MoviePhoto
                        {
                            Id = Guid.NewGuid(),
                            MovieId = movie.Id,
                            Data = memoryStream.ToArray(),
                            ContentType = file.ContentType,
                        };
                        await _movieRepository.AddMoviePhotoAsync(MoviePhoto);
                    }
                }
            }
        }
        public async Task<AddMovieViewModel> GetAddMovieViewDataAsync()
        {
             var genres = await _context.Genre.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToListAsync();

            var languages = _enumServices.GetEnumSelectList<Langue>();

            var subtitles = new List<SelectListItem>
        {
            new SelectListItem { Text = "Yes", Value = "true" },
            new SelectListItem { Text = "No", Value = "false" }
        };

            return new AddMovieViewModel
            {
                Genres = genres,
                Languages = languages,
                Subtitles = subtitles
            };
        }
        public async Task<List<Movie>> GetAllMoviesWithPhotosAsync()
        {
            return await _movieRepository.GetAllMoviesWithPhotosAsync();
        }
        public async Task AddSalonAsync(TheatreSalon salon, string clickedCells)
        {
            if(await _salonRepository.IsSalonNumberExistsAsync(salon.SalonNumber))
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
            salon.Capacity = salon.Rows+salon.Columns-salon.EmptySeatsCoords.Count;
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
