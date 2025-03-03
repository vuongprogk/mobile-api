using Microsoft.EntityFrameworkCore;
using mobile_api.Data;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ILogger<TourRepository> _logger;
        private readonly ApplicationDbContext _context;
        public TourRepository(ILogger<TourRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<bool> AddTourAsync(Tour tour)
        {
            _logger.LogInformation($"{nameof(TourRepository)} action: {nameof(AddTourAsync)}");
            await _context.Tours.AddAsync(tour);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTourAsync(string id)
        {
            _logger.LogInformation($"{nameof(TourRepository)} action: {nameof(DeleteTourAsync)}");
            var tour = await _context.Tours.FirstOrDefaultAsync(item => item.Id == id);
            if (tour == null)
            {
                return false;
            }
            _context.Tours.Remove(tour);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<Tour> GetTourByIdAsync(string id)
        {
            _logger.LogInformation($"{nameof(TourRepository)} action: {nameof(GetTourByIdAsync)}");
            return _context.Tours.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<IEnumerable<Tour>> GetToursAsync()
        {
            _logger.LogDebug($"{nameof(TourRepository)} action: {nameof(GetToursAsync)}");
            return await _context.Tours.ToListAsync();
        }

        public async Task<bool> UpdateTourAsync(Tour tour)
        {
            _logger.LogInformation($"{nameof(TourRepository)} action: {nameof(UpdateTourAsync)}");
            _context.Tours.Update(tour);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
