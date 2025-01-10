using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Repositories.IRepositories;

namespace MyCinema.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly MyCinemaDBContext _context;
        public LanguageRepository(MyCinemaDBContext context) 
        {
            _context = context;
        }
        public async Task<Language> GetLanguageNameByIsoCodeAsync(string isoCode)
        {
            return _context.Language.Where(l=>l.iso_code == isoCode).FirstOrDefault();
        }

    }
}
