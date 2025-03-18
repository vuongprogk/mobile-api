using mobile_api.Models;
using mobile_api.Repositories.Interfaces;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _service;
        private readonly ILogger<ServiceService> _logger;
        public ServiceService(IServiceRepository service, ILogger<ServiceService> logger)
        {
            _logger = logger;
            _service = service;
        }
        public async Task<Service> GetServiceById(string id)
        {
            _logger.LogInformation($"{nameof(ServiceService)} action: {nameof(GetServiceById)}");
            return await _service.GetServiceByIdAsync(id);
        }

        public async Task<IEnumerable<Service>> GetServiceByTourId(string id)
        {
            _logger.LogInformation($"{nameof(ServiceService)} action: {nameof(GetServiceByTourId)}");
            return await _service.GetServicesByTourIdAsync(id);
        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            _logger.LogInformation($"{nameof(ServiceService)} action: {nameof(GetServices)}");
            return await _service.GetServicesAsync();
        }

        public async Task<bool> CreateService(Service service)
        {
            _logger.LogInformation($"{nameof(ServiceService)} action: {nameof(CreateService)}");
            return await _service.AddServiceAsync(service);
        }

        public async Task<bool> UpdateService(Service service)
        {
            _logger.LogInformation($"{nameof(ServiceService)} action: {nameof(UpdateService)}");
            return await _service.UpdateServiceAsync(service);
        }
    }
}
