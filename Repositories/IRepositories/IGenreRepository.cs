using MyCinema.Data;

namespace MyCinema.Repositories.IRepositories
{
    public interface IGenreRepository
    {
        Task<Genre> GetGenreByName(string name);
    }
}
