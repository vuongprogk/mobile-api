﻿using Microsoft.EntityFrameworkCore;
using mobile_api.Data;
using mobile_api.Dtos.Service;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _service;
        private readonly ITourRepository _tour;
        private readonly ILogger<ServiceService> _logger;
        private readonly ApplicationDbContext _context;
        public ServiceService(IServiceRepository service, ILogger<ServiceService> logger, ITourRepository tour, ApplicationDbContext context)
        {
            _tour = tour;
            _logger = logger;
            _service = service;
            _context = context;
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

        public async Task<bool> UpdateTourService(string id, UpdateTourServiceRequest request)
        {
            _logger.LogInformation($"{nameof(ServiceService)} action: {nameof(UpdateTourService)}");
            var tour = await _context.Tours.Include(tour => tour.Services).FirstOrDefaultAsync(item => item.Id == id);
            if (tour == null)
            {
                return false;
            }
            // empty service list of tour first
            tour.Services.Clear();
            // add new service list to tour
           foreach (var serviceId in request.ServiceId)
            {
                var service = await _context.Services.FirstOrDefaultAsync(item => item.Id == serviceId);
                if (service == null)
                {
                    return false;
                }
                tour.Services.Add(service);
            }
            _context.Tours.Update(tour);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
