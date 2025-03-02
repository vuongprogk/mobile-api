using mobile_api.Models;

namespace mobile_api.Repositories.Interfaces
{
    public interface ITourRepository
    {
        public Task<Tour> GetTourByIdAsync(string id);
        public Task<IEnumerable<Tour>> GetToursAsync();
        public Task<bool> AddTourAsync(Tour tour);
        public Task<bool> UpdateTourAsync(Tour tour);
        public Task<bool> DeleteTourAsync(string id);
    }
}
