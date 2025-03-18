using mobile_api.Models;
using mobile_api.Repositories.Interfaces;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<TicketService> _logger;

        public TicketService(ITicketRepository ticketRepository, ILogger<TicketService> logger)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;
        }

        public async Task<bool> CreateTicket(Ticket ticket)
        {
            _logger.LogInformation($"{nameof(TicketService)} action: {nameof(CreateTicket)}");
            return await _ticketRepository.AddTicketAsync(ticket);
        }

        public async Task<Ticket> GetTicketById(string id)
        {
            _logger.LogInformation($"{nameof(TicketService)} action: {nameof(GetTicketById)}");
            return await _ticketRepository.GetTicketByIdAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            _logger.LogInformation($"{nameof(TicketService)} action: {nameof(GetTickets)}");
            return await _ticketRepository.GetTicketsAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserId(string userId)
        {
            _logger.LogInformation($"{nameof(TicketService)} action: {nameof(GetTicketsByUserId)}");
            return await _ticketRepository.GetTicketByUserIdAsync(userId);
        }
    }
}
