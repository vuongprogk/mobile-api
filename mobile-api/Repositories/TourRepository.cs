using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class TourRepository : ITourRepository
    {
        public Task<bool> AddTourAsync(Tour tour)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTourAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Tour> GetTourByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tour>> GetToursAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTourAsync(Tour tour)
        {
            throw new NotImplementedException();
        }
    }
}
