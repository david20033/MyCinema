using MyCinema.Data;
using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface IAdminService
    {
        Task<List<AppSetting>> GetAppSettingsAsync();
        Task UpdateSetting(string key, string value);
        Task InsertLanguagesInDB();
        Task InsertGenresInDB();
        Task SeedDb();
    }
}
