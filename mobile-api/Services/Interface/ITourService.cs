using mobile_api.Models;

namespace mobile_api.Services.Interface
{
    public interface ITourService
    {
        Task<bool> CreateTour(Tour tour);
        Task<Tour> GetTourById(string id);
        Task<IEnumerable<Tour>> GetTours();
    }
}
