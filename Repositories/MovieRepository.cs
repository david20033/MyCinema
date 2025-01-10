using Microsoft.EntityFrameworkCore;
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
            if(movie == null)
            {
                return;
            }
            var existingMovie = await _context.Movie
                                       .FirstOrDefaultAsync(m => m.Imdb_id == movie.Imdb_id);
            if (existingMovie == null)
            {
                _context.Movie.Add(movie);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddMoviePhotoAsync(MoviePhoto moviePhoto)
        {
            _context.MoviePhoto.Add(moviePhoto);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Movie>> GetAllMoviesWithPhotosAsync()
        {
            return await _context.Movie.Include(m=>m.MoviePhotos)
                .ToListAsync();
        }
        public async Task<Movie> GetMovieWithPhotosByIdAsync(Guid id)
        {
            return await _context.Movie
                .Where(m => m.Id == id)
                .Include(m => m.MoviePhotos).FirstOrDefaultAsync();
        }
        public async Task<Movie> GetMovieDetailsByIdAsync(Guid id)
        {
            return await _context.Movie
                .Where(m => m.Id == id)
                .Include(m=> m.Genres)
                .ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync();
        }
    }
}
