using MyCinema.Data;
using System.ComponentModel.DataAnnotations;

namespace MyCinema.ViewModels
{
    public class AddScreeningViewModel
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public Guid TheatreSalonId { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal TicketPrice { get; set; }

        public List<TheatreSalon> TheatreSalons { get; set; }
        public List<Movie> Movies { get; set; }

    }
}
