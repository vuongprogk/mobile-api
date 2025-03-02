using mobile_api.Models;

namespace mobile_api.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        public Task<Service> GetServiceByIdAsync(string id);
        public Task<IEnumerable<Service>> GetServicesAsync();
        public Task<IEnumerable<Service>> GetServicesByTourIdAsync(string tourId);
        public Task<bool> AddServiceAsync(Service service);
        public Task<bool> UpdateServiceAsync(Service service);
        public Task<bool> DeleteServiceAsync(string id);
    }
}
