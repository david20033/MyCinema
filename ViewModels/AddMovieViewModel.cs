using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyCinema.ViewModels
{
    public class AddMovieViewModel
    {
        public IEnumerable<SelectListItem> Genres { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
        public IEnumerable<SelectListItem> Subtitles { get; set; }
    }
}
