namespace MyCinema.Services.IServices
{
    public interface ILanguageRepository
    {
        Task<string> GetLanguageNameByIsoCodeAsync(string isoCode);
    }
}
