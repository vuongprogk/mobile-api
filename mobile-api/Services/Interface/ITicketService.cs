using mobile_api.Dtos.Ticket;
using mobile_api.Models;

namespace mobile_api.Services.Interface
{
    public interface ITicketService
    {
        Task<bool> CreateTicket(Ticket ticket);
        Task<Ticket> GetTicketById(string id);
        Task<IEnumerable<Ticket>> GetTickets();
    }
}
