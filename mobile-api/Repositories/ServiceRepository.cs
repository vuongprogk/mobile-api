using Microsoft.EntityFrameworkCore;
using mobile_api.Data;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ILogger<ServiceRepository> _logger;
        private readonly ApplicationDbContext _context;
        public ServiceRepository(ApplicationDbContext context, ILogger<ServiceRepository> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<bool> AddServiceAsync(Service service)
        {
            _logger.LogInformation($"{nameof(ServiceRepository)} action: {nameof(AddServiceAsync)}");
            await _context.Services.AddAsync(service);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteServiceAsync(string id)
        {
            _logger.LogInformation($"{nameof(ServiceRepository)} action: {nameof(DeleteServiceAsync)}");
            var service = await _context.Services.FirstOrDefaultAsync(item => item.Id == id);
            if (service == null)
            {
                return false;
            }
            _context.Services.Remove(service);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Service> GetServiceByIdAsync(string id)
        {
            _logger.LogInformation($"{nameof(ServiceRepository)} action: {nameof(GetServiceByIdAsync)}");
            return await _context.Services.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            _logger.LogInformation($"{nameof(ServiceRepository)} action: {nameof(GetServicesAsync)}");
            return await _context.Services.ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByTourIdAsync(string tourId)
        {
            _logger.LogInformation($"{nameof(ServiceRepository)} action: {nameof(GetServicesByTourIdAsync)}");
            if (string.IsNullOrEmpty(tourId))
            {
                return null;
            }
            var tour = await _context.Tours.FirstOrDefaultAsync(item => item.Id == tourId);
            
            var services = _context.Services.Where(item => item.Tours.Contains(tour));
            return await services.ToListAsync();
        }

        public async Task<bool> UpdateServiceAsync(Service service)
        {
            _logger.LogInformation($"{nameof(ServiceRepository)} action: {nameof(UpdateServiceAsync)}");
            _context.Services.Update(service);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
