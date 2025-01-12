using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Helpers;
using MyCinema.Repositories.IRepositories;
using System.Drawing.Printing;

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
        public async Task<List<Movie>> GetMoviesAsync(int pageNumber)
        {
            int pageSize = 20;  

            return await _context.Movie
                .Include(l=>l.Original_language)
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize)                   
                .ToListAsync();
        }
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movie
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
        public async Task<int> GetMoviesCount()
        {
            return _context.Movie.Count();
        }
        public async Task<List<Movie>> GetAllUnprojectedScreenings(QueryObject query)
        {
            var currentTime = DateTime.Now;

            var movies = await _context.Movie
                    .Where(m => m.Screenings.Any(s => s.StartTime > currentTime)) 
                    .Include(m => m.Screenings.Where(s => s.StartTime > currentTime)) 
                    .Include(m => m.Genres)
                    .ThenInclude(g => g.Genre)
                    .ToListAsync();
            if (query.Date != null)
            {
                movies = movies.Where(m => m.Screenings.Any(s => s.StartTime.Date == query.Date.Value.Date)).ToList();
            }
            return movies;
        }
    }
}
