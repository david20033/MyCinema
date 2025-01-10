using MyCinema.Data;

namespace MyCinema.Repositories.IRepositories
{
    public interface ILanguageRepository
    {
        Task<Language> GetLanguageNameByIsoCodeAsync(string isoCode);
    }
}
