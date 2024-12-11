using MyCinema.Data;
using MyCinema.Repositories.IRepositories;

namespace MyCinema.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MyCinemaDBContext _context;
        public MovieRepository(MyCinemaDBContext context) 
        {
            _context = context;
        }
        public async Task AddMovieAsync(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();
        }
        public async Task AddMoviePhotoAsync(MoviePhoto moviePhoto)
        {
            _context.MoviePhoto.Add(moviePhoto);
            await _context.SaveChangesAsync();
        }
    }
}
