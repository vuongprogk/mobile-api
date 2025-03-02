using mobile_api.Models;

namespace mobile_api.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        public Task<Ticket> GetTicketByUserIdAsync(string id);
        public Task<IEnumerable<Ticket>> GetTicketsAsync();
        public Task<bool> AddTicketAsync(Ticket ticket);
        public Task<bool> UpdateTicketAsync(Ticket ticket);
        public Task<bool> DeleteTicketAsync(string id);
    }
}
