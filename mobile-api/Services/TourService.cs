using mobile_api.Models;
using mobile_api.Repositories.Interfaces;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tour;
        private readonly ILogger<TourService> _logger;
        public TourService(ITourRepository tourRepository, ILogger<TourService> logger)
        {
            _logger = logger;
            _tour = tourRepository;
        }
        public async Task<bool> CreateTour(Tour tour)
        {
            _logger.LogInformation($"{nameof(TourService)} action: {nameof(CreateTour)}");
            return await _tour.AddTourAsync(tour);
        }

        public async Task<Tour> GetTourById(string id)
        {
            _logger.LogInformation($"{nameof(TourService)} action: {nameof(GetTourById)}");
            return await _tour.GetTourByIdAsync(id);
        }

        public async Task<IEnumerable<Tour>> GetTours()
        {
            _logger.LogInformation($"{nameof(TourService)} action: {nameof(GetTours)}");
            return await _tour.GetToursAsync();
        }

        public async Task<bool> UpdateTour(Tour tour)
        {
            _logger.LogInformation($"{nameof(TourService)} action: {nameof(UpdateTour)}");
            return await _tour.UpdateTourAsync(tour);
        }

        public async Task<bool> UpdateTourCategoriesAndTags(string tourId, List<int> categoryIds, List<int> tagIds)
        {
            _logger.LogInformation($"{nameof(TourService)} action: {nameof(UpdateTourCategoriesAndTags)}");
            return await _tour.UpdateTourCategoriesAndTagsAsync(tourId, categoryIds, tagIds);
        }
    }
}
