﻿using Microsoft.EntityFrameworkCore;
using MyCinema.Data;
using MyCinema.Models;
using MyCinema.Repositories.IRepositories;

namespace MyCinema.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly MyCinemaDBContext _context;
        public TicketRepository(MyCinemaDBContext context)
        {
            _context = context;
        }
        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Ticket.ToListAsync();
        }
        public async Task AddTicketOrderAsync(TicketOrder ticket)
        {
            await _context.TicketOrder.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        public async Task<TicketOrder> GetTicketOrderByIdAsync(Guid id)
        {
            return await _context.TicketOrder.Where(t => t.Id == id)
                .Include(t=>t.Tickets)
                .ThenInclude(t=>t.Screening)
                .ThenInclude(t=>t.TheatreSalon)
                .Include(t => t.Tickets)
                .ThenInclude(t => t.Screening)
                .ThenInclude(t=>t.Movie)
                .ThenInclude(t=>t.Original_language)
                .Include(t => t.Tickets)
                .ThenInclude(t => t.Screening)
                .ThenInclude(t => t.Movie)
                .ThenInclude(t => t.Genres)
                .ThenInclude(t=>t.Genre)
                .FirstOrDefaultAsync();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<TicketSummary>> GetTicketSummaryByTicketOrderId(Guid id)
        {
            return await _context.Ticket
                .Where(t=> t.TicketOrderId == id)
                .GroupBy(t=> new {t.Type, t.Price})
                .Select(g => new TicketSummary
                {
                    Type = g.Key.Type,
                    Quantity = g.Count(),
                    PricePerTicket = g.Key.Price,
                })
                .ToListAsync();
        }
        public async Task<bool> IsTicketOrderExist(Guid id)
        {
            var order = await _context.TicketOrder.Where(t=>t.Id == id).FirstOrDefaultAsync();
            if(order == null)
            {
                return false;
            }
            return true;
        }
        public async Task<List<AppSetting>> GetAppSettingsAsync()
        {
            return await _context.AppSetting.ToListAsync();
        }
    }
}
