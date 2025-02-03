using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private readonly IApiService _apiService;
        private readonly IMovieService _movieService;
        private readonly MyCinemaDBContext _context;
        private readonly ISalonService _salonService;
        private readonly IScreeningService _screeningService;
        public AdminService (MyCinemaDBContext context, IApiService apiService, IMovieService movieService, ISalonService salonService, IScreeningService screeningService)
        {
            _context = context;
            _apiService = apiService;
            _movieService = movieService;
            _salonService = salonService;
            _screeningService = screeningService;
        }
        public async Task<List<AppSetting>> GetAppSettingsAsync()
        {
            return await _context.AppSetting.ToListAsync();
        }
        public async Task UpdateSetting(string key, string value)
        {
            var setting = _context.AppSetting.FirstOrDefault(s => s.Key == key);
            if (setting == null)
            {
                return;
            }
            if(setting.Key== "VipTicketPrice"||setting.Key== "RegularTicketPrice") 
            {
                if (int.Parse(value) <= 0)
                {
                    return;
                }
            }
            setting.Value = value;
            await _context.SaveChangesAsync();
        }
        public async Task InsertLanguagesInDB()
        {
            if (_context.Language.Any()) { return;}
            var Languages = await _apiService.GetLanguagesAsync();

            var LanguageList = Languages.Select(dto => new Language
            {
                Id = Guid.NewGuid(),
                iso_code = dto.iso_639_1,
                English_Name = dto.english_name,
                Name = dto.name
            }).ToList();

            await _context.Language.AddRangeAsync(LanguageList);
            await _context.SaveChangesAsync();
        }
        public async Task InsertGenresInDB()
        {
            if(_context.Genre.Any()) { return;}
            var Genres = await _apiService.GetGenresAsync();

            var GenresList = Genres.Select(dto => new Genre
            {
                Id = Guid.NewGuid(),
                Name = dto.name
            }).ToList();
            await _context.Genre.AddRangeAsync(GenresList);
            await _context.SaveChangesAsync();
        }
        public async Task SeedDb()
        {
            try
            {
                await DbInitializer.SeedAsync(_context, _apiService,_movieService,_salonService,this,_screeningService);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
