using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;

namespace MyCinema.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IScreeningRepository _screeningRepository;
        public TicketService (ITicketRepository ticketRepository, IScreeningRepository screeningRepository)
        {
            _ticketRepository = ticketRepository;
            _screeningRepository = screeningRepository;
        }
        public async Task<SelectTicketViewModel> GetSelectTicketViewModel(Guid ScreeningId)
        {
            var Screening = await _screeningRepository.GetScreeningByIdAsync(ScreeningId);
            var TicketTypes = await _ticketRepository.GetAllTickets();
            var TicketViewModel = new List<TicketTypeViewModel>();
            foreach (var t in TicketTypes)
            {
                var ticket = new TicketTypeViewModel
                {
                    Name = t.Type.ToString(),
                    Price = t.Price,
                    Quantity = 0
                };
                TicketViewModel.Add(ticket);
            }
            return new SelectTicketViewModel
            {
                PosterPath = Screening.Movie.Poster_path,
                Title = Screening.Movie.Title,
                Language = Screening.Movie.Original_language?.Name,
                Genres = Screening.Movie.Genres?.Select(g => g.Genre.Name).ToList(),
                SalonNumber = Screening.TheatreSalon.SalonNumber,
                ShowTime = Screening.StartTime,
                TicketTypes = TicketViewModel
            };
        }
        public async Task AddTicketAsync(Ticket ticket)
        {
            await _ticketRepository.AddTicketAsync(ticket);
        }
    }
}
