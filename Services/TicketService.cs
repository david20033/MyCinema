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
                ReservedSeatsCoords = Order.Tickets[0].Screening.ReservedSeats,
                TicketCount = Order.Tickets.Count,
                TicketOrderId = id
            };
        }
        public async Task SeedSeatsCoordsWithTicketOrder(SelectSeatsViewModel model)
        {
            var Order = await _ticketRepository.GetTicketOrderByIdAsync(model.TicketOrderId);
            var Seats = JsonSerializer.Deserialize<List<string>>(model.SeatCoords);
            var Screening = Order.Tickets[0].Screening;
            for (int i = 0; i < Order.Tickets.Count; i++)
            {
                Order.Tickets[i].SeatNumber = Seats[i];
                Screening.ReservedSeats.Add(Seats[i]);
            }
            await _ticketRepository.SaveAsync();
        }
        public async Task<ConfirmOrderViewModel> GetConfirmOrderViewModel(Guid id)
        {
            var order = await _ticketRepository.GetTicketOrderByIdAsync(id);
            var Movie = order.Tickets[0].Screening.Movie;
            var Screening = order.Tickets[0].Screening;
            var model = new ConfirmOrderViewModel
            {
                PosterPath = Movie.Poster_path,
                Title = Movie.Title,
                ReleaseYear = DateTime.Parse(Movie.Release_date),
                ShowStartTime = Screening.StartTime,
                ShowEndTime = Screening.EndTime,
                Language = Movie.Original_language.English_Name,
                Genres = Movie.Genres.Select(g => g.Genre.Name).ToList(),
                SalonNumber = Screening.TheatreSalon.SalonNumber,
                RegularTicketPrice = 12,
                VipTicketPrice = 15,
                RegularTicketSeatsCoords = order.Tickets.Where(t => t.Type == Enums.TicketType.Regular).Select(t => t.SeatNumber).ToList(),
                VipTicketSeatsCoords = order.Tickets.Where(t => t.Type == Enums.TicketType.VIP).Select(t => t.SeatNumber).ToList(),
                TicketOrderId = id
            };
            return model; 

        }
        public async Task AddUserIdInTickerOrder(Guid TicketOrderId, string UserId)
        {
            var order = await _ticketRepository.GetTicketOrderByIdAsync(TicketOrderId);
            order.CustomerId = Guid.Parse(UserId);
            await _ticketRepository.SaveAsync();
        }
    }
}
