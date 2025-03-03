using mobile_api.Models;

namespace mobile_api.Services.Interface
{
    public interface IUserService
    {
        public Task<User> GetUserByUsernameAsync(string username);
        public Task<bool> AddUserAsync(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> UpdateUser(User user, string id);
        Task<bool> CreateUser(User user);
        Task<User> GetUserById(string id);
    }
}
