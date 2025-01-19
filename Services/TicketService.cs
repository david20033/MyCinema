using System.Text.Json;
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
            return new SelectTicketViewModel
            {
                PosterPath=Screening.Movie.Poster_path,
                Title = Screening.Movie.Title,
                Language = Screening.Movie.Original_language?.English_Name,
                Genres = Screening.Movie.Genres.Select(g=>g.Genre.Name).ToList(),
                SalonNumber = Screening.TheatreSalon.SalonNumber,
                ShowTime = Screening.StartTime,
                RegularTicketPrice = 12,
                VipTicketPrice = 15,
                ScreeningId = ScreeningId
            };
        }
        public async Task<Guid> AddTicketOwnershipInDbAsync(SelectTicketViewModel model)
        {
            //screeningId, vip count regular count
            var Screening = await _screeningRepository.GetScreeningByIdAsync(model.ScreeningId);
            List<Ticket> Tickets = [];
            var TicketOrder = new TicketOrder()
            {
                Id=Guid.NewGuid(),
                OrderDate=DateTime.Now,
            };
            for(int i=0;i<model.RegularTicketCount; i++)
            {
                Tickets.Add(new Ticket
                {
                    Id = Guid.NewGuid(),
                    Price = model.RegularTicketPrice,
                    Type = Enums.TicketType.Regular,
                    ScreeningId = Screening.Id,
                    TicketOrderId = TicketOrder.Id,
                    TicketOrder=TicketOrder,
                    SeatNumber = "-1"
                });
            }
            for (int i = 0; i < model.VipTicketCount; i++)
            {
                Tickets.Add(new Ticket
                {
                    Id = Guid.NewGuid(),
                    Price = model.VipTicketPrice,
                    Type = Enums.TicketType.VIP,
                    ScreeningId = Screening.Id,
                    TicketOrderId = TicketOrder.Id,
                    TicketOrder = TicketOrder,
                    SeatNumber = "-1"
                });
            }
            TicketOrder.Tickets = Tickets;
            await _ticketRepository.AddTicketOrderAsync(TicketOrder);
            return TicketOrder.Id;
        }
        public async Task<SelectSeatsViewModel> GetSelectSeatsViewModel(Guid id)
        {
            var Order = await _ticketRepository.GetTicketOrderByIdAsync(id);
            var Salon = Order.Tickets[0].Screening.TheatreSalon;
            return new SelectSeatsViewModel
            {
                SalonColumns = Salon.Columns,
                SalonRows = Salon.Rows,
                EmptySeatsCoords = Salon.EmptySeatsCoords,
                TicketCount = Order.Tickets.Count,
                TicketOrderId = id
            };
        }
        public async Task SeedSeatsCoordsWithTicketOrder(SelectSeatsViewModel model)
        {
            var Order = await _ticketRepository.GetTicketOrderByIdAsync(model.TicketOrderId);
            var Seats = JsonSerializer.Deserialize<List<string>>(model.SeatCoords);
            for (int i = 0; i < Order.Tickets.Count; i++)
            {
                Order.Tickets[i].SeatNumber = Seats[i];
            }
            await _ticketRepository.SaveAsync();
        }
    }
}
