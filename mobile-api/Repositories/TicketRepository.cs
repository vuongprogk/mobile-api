using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public Task<bool> AddTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTicketAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetTicketByUserIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
