using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
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
    }
}
