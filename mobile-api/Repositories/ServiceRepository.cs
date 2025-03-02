using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        public Task<bool> AddServiceAsync(Service service)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteServiceAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Service> GetServiceByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Service>> GetServicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Service>> GetServicesByTourIdAsync(string tourId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateServiceAsync(Service service)
        {
            throw new NotImplementedException();
        }
    }
}
