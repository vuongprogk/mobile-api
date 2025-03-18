using Microsoft.EntityFrameworkCore;
using mobile_api.Data;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ILogger<TicketRepository> _logger;
        private readonly ApplicationDbContext _context;
        public TicketRepository(ILogger<TicketRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<bool> AddTicketAsync(Ticket ticket)
        {
            _logger.LogInformation($"{nameof(TicketRepository)} action: {nameof(AddTicketAsync)}");
            await _context.Tickets.AddAsync(ticket);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTicketAsync(string id)
        {
            _logger.LogInformation($"{nameof(TicketRepository)} action: {nameof(DeleteTicketAsync)}");
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return false;
            }
            _context.Tickets.Remove(ticket);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Ticket> GetTicketByIdAsync(string id)
        {
            _logger.LogInformation($"{nameof(TicketRepository)} action: {nameof(GetTicketByIdAsync)}");
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Invalid ticket ID provided");
                return null;
            }
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetTicketByUserIdAsync(string id)
        {
            _logger.LogInformation($"{nameof(TicketRepository)} action: {nameof(GetTicketByUserIdAsync)}");
            return await _context.Tickets.Where(item => item.UserId == id).ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            _logger.LogInformation($"{nameof(TicketRepository)} action: {nameof(GetTicketsAsync)}");
            return await _context.Tickets.ToListAsync();
        }

        public async Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            _logger.LogInformation($"{nameof(TicketRepository)} action: {nameof(UpdateTicketAsync)}");
            _context.Tickets.Update(ticket);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
