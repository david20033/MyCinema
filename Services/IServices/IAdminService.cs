using MyCinema.Data;
using MyCinema.ViewModels;

namespace MyCinema.Services.IServices
{
    public interface IAdminService
    {
        Task AddMovieWithPhotosAsync(Movie movie, List<IFormFile> MoviePhotos);
        Task<AddMovieViewModel> GetAddMovieViewDataAsync();
    }
}
