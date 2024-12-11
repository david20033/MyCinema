using System.Security.Claims;
using MyCinema.Data;
using MyCinema.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCinema.Services;

namespace MyCinema.Controllers
{
    public class AdminController : Controller
    {
        private readonly EnumServices _enumServices;
        private readonly MyCinemaDBContext _context;
        public AdminController(MyCinemaDBContext context, EnumServices EnumServices)
        {
            _enumServices = EnumServices;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddMovie()
        {
            ViewData["Lange"] = _enumServices.GetEnumSelectList<Langue>();
            ViewData["Genre"] = new SelectList(_context.Genre, "Id", "Name");
            ViewData["Subs"] = new SelectList(new List<SelectListItem>
                  {
        new SelectListItem { Text = "Yes", Value = "true" },
        new SelectListItem { Text = "No", Value = "false" }
    }, "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie(Movie movie, List<IFormFile> MoviePhotos)
        {
            _context.Movie.Add(movie);
             await _context.SaveChangesAsync();
            if (MoviePhotos != null && MoviePhotos.Any())
            {
                foreach (var file in MoviePhotos)
                {
                    if (file.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);

                        var MoviePhoto = new MoviePhoto
                        {
                            Id = Guid.NewGuid(),
                            MovieId = movie.Id,
                            Data = memoryStream.ToArray(),
                            ContentType = file.ContentType,
                        };
                        _context.MoviePhoto.Add(MoviePhoto);
                        await _context.SaveChangesAsync();

                    }
                     _context.SaveChanges();
                }
            }
            return View("Index");
        }
    }
}
