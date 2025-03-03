using mobile_api.Models;
using mobile_api.Repositories.Interfaces;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticket;
        private readonly ILogger<TicketService> _logger;
        public TicketService(ITicketRepository ticketRepository, ILogger<TicketService> logger)
        {
            _logger = logger;
            _ticket = ticketRepository;
        }
        public async Task<bool> CreateTicket(Ticket ticket)
        {
            _logger.LogInformation($"{nameof(TicketService)} action: {nameof(CreateTicket)}");
            return await _ticket.AddTicketAsync(ticket);
        }

        public async Task<Ticket> GetTicketById(string id)
        {
            _logger.LogInformation($"{nameof(TicketService)} action: {nameof(GetTicketById)}");
            return await _ticket.GetTicketByIdAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            _logger.LogInformation($"{nameof(TicketService)} action: {nameof(GetTickets)}");
            return await _ticket.GetTicketsAsync();
        }
    }
}
