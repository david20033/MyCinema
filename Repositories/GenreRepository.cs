using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Repositories.IRepositories;

namespace MyCinema.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MyCinemaDBContext _context;
        public GenreRepository(MyCinemaDBContext context)
        {
            _context = context;
        }
        public async Task<Genre> GetGenreByName(string name)
        {
            return await _context.Genre.Where(g => g.Name == name).FirstOrDefaultAsync();
        }
    }
}
