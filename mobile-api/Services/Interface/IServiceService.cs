using mobile_api.Models;

namespace mobile_api.Services.Interface
{
    public interface IServiceService
    {
        Task<Service> GetServiceById(string id);
        Task<IEnumerable<Service>> GetServiceByTourId(string id);
        Task<IEnumerable<Service>> GetServices();
        Task<bool> CreateService(Service service);
        Task<bool> UpdateService(Service service);
    }
}
