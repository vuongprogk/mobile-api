using mobile_api.Dtos.User;
using mobile_api.Models;

namespace mobile_api.Services.Interface
{
    public interface IAuthService
    {
        public Task<bool> RegisterAsync(RegisterRequest user);
        public Task<string?> LoginAsync(LoginRequest login);
        public Task<User> GetUserByUsernameAsync(string username);
    }
}
