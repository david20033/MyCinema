﻿using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Helpers;
using MyCinema.Repositories.IRepositories;

namespace MyCinema.Repositories
{
    public class SalonRepository : ISalonRepository
    {
        private readonly MyCinemaDBContext _context;
        public SalonRepository(MyCinemaDBContext context)
        {
            _context = context;
        }
        public async Task AddSalonAsync(TheatreSalon salon)
        {
            _context.TheatreSalon.Add(salon);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsSalonNumberExistsAsync(int salonNumber)
        {
            return await _context.TheatreSalon.AnyAsync(s=>s.SalonNumber == salonNumber);
        }
        public async Task<List<TheatreSalon>> GetTheatreSalonsAsync()
        {
            return await _context.TheatreSalon
                .OrderBy(x=>x.SalonNumber)
                .Include(ts=>ts.Screenings)
                .ThenInclude(ts=>ts.Movie)
                .ToListAsync();
        }
        public async Task<TheatreSalon> GetTheatreSalonByIdAsync(Guid id)
        {
            return await _context.TheatreSalon.Where(s=>s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<TimeSpan> GetCinemaOpenTimeAsync()
        {
            var result = await _context.AppSetting.Where(k => k.Key == "CinemaOpenHour").FirstOrDefaultAsync();
            return TimeSpan.Parse(result.Value);

        }
        public async Task<TimeSpan> GetCinemaCloseTimeAsync()
        {
            var result = await _context.AppSetting.Where(k => k.Key == "CinemaCloseHour").FirstOrDefaultAsync();
            return TimeSpan.Parse(result.Value);
        }
    }
}
