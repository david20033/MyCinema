using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyCinema.Data;
using MyCinema.Models;
using MyCinema.Repositories.IRepositories;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;
using Stripe.Checkout;

namespace MyCinema.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IScreeningRepository _screeningRepository;
        private readonly IStripeService _stripeService;
        public TicketService (ITicketRepository ticketRepository, IScreeningRepository screeningRepository, IStripeService stripeService)
        {
            _ticketRepository = ticketRepository;
            _screeningRepository = screeningRepository;
            _stripeService = stripeService;        
        }
        public async Task<SelectTicketViewModel> GetSelectTicketViewModel(Guid ScreeningId)
        {
            var Screening = await _screeningRepository.GetScreeningByIdAsync(ScreeningId);
            var Settings = await _ticketRepository.GetAppSettingsAsync();
            return new SelectTicketViewModel
            {
                PosterPath=Screening.Movie.Poster_path,
                Title = Screening.Movie.Title,
                Language = Screening.Movie.Original_language?.English_Name,
                Genres = Screening.Movie.Genres.Select(g=>g.Genre.Name).ToList(),
                SalonNumber = Screening.TheatreSalon.SalonNumber,
                ShowTime = Screening.StartTime,
                RegularTicketPrice = int.Parse(Settings.Where(k=>k.Key== "RegularTicketPrice").FirstOrDefault().Value),
                VipTicketPrice = int.Parse(Settings.Where(k => k.Key == "VipTicketPrice").FirstOrDefault().Value),
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
        public async Task UnSeedSeatsCoordsWithTicketOrder(Guid TicketOrderId)
        {
            var Order = await _ticketRepository.GetTicketOrderByIdAsync(TicketOrderId);
            if(Order.Tickets.Count == 0) return;
            var Screening = Order.Tickets[0].Screening;
            foreach ( var ticket in Order.Tickets)
            {
                Screening.ReservedSeats.Remove(ticket.SeatNumber);
                ticket.SeatNumber = "-1";
            }
            await _ticketRepository.SaveAsync();
        }
        public async Task<ConfirmOrderViewModel> GetConfirmOrderViewModel(Guid id)
        {
            var order = await _ticketRepository.GetTicketOrderByIdAsync(id);
            var Settings = await _ticketRepository.GetAppSettingsAsync();
            if (order.CustomerId != Guid.Empty) return null;
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
                RegularTicketPrice = int.Parse(Settings.Where(k => k.Key == "RegularTicketPrice").FirstOrDefault().Value),
                VipTicketPrice = int.Parse(Settings.Where(k => k.Key == "VipTicketPrice").FirstOrDefault().Value),
                RegularTicketSeatsCoords = order.Tickets.Where(t => t.Type == Enums.TicketType.Regular).Select(t => t.SeatNumber).ToList(),
                VipTicketSeatsCoords = order.Tickets.Where(t => t.Type == Enums.TicketType.VIP).Select(t => t.SeatNumber).ToList(),
                TicketOrderId = id
            };
            return model; 

        }
        public async Task ConfirmTicketOrder(Guid TicketOrderId, string UserId)
        {
            var order = await _ticketRepository.GetTicketOrderByIdAsync(TicketOrderId);
            order.CustomerId = Guid.Parse(UserId);
            foreach (var ticket in order.Tickets)
            {
                ticket.Screening.Movie.Profit += ticket.Price;
                ticket.Screening.Movie.TicketSoldCount++;
            }
            await _ticketRepository.SaveAsync();
        }
        public async Task<bool> IsTicketOrderExists(Guid Id)
        {
            return await _ticketRepository.IsTicketOrderExist(Id);
        }
        public async Task<Session> CreateStripeSession(Guid OrderId)
        {
            var Tickets =  await _ticketRepository.GetTicketSummaryByTicketOrderId(OrderId);
            var LineItems = new List<SessionLineItemOptions>();
            foreach (var ticket in Tickets)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)ticket.PricePerTicket * 100,
                        Currency = "BGN",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{ticket.Type} Ticket"
                        }
                    },
                    Quantity = ticket.Quantity,
                };
                LineItems.Add(sessionListItem);
            }
            var domain = "https://localhost:7128";
            return _stripeService.CreateCheckoutSession($"{domain}/Ticket/Success?OrderId={OrderId}", $"{domain}/Ticket/Cancel", LineItems);
        }
    }
}
