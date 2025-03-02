using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<bool> AddNewUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
