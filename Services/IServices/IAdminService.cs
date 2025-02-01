using MyCinema.Data;
using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface IAdminService
    {
        Task InsertLanguagesInDB();
        Task InsertGenresInDB();
        Task SeedDb();
    }
}
