using Mapster;
using mobile_api.Dtos.User;
using mobile_api.Models;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;
        public AuthService(IUserService user,ITokenService tokenService, ILogger<AuthService> logger)
        {
            _logger = logger;
            _userService = user;
            _tokenService = tokenService;
        }
        public async Task<string> LoginAsync(LoginRequest login)
        {
            var isExits = await _userService.GetUserByUsernameAsync(login.Username);
            if (isExits == null)
            {
                return null;
            }
            var verify = BCrypt.Net.BCrypt.Verify(login.Password, isExits.HashPassword);
            if (!verify)
            {
                return null;
            }
            return _tokenService.CreateToken(isExits);
        }

        public async Task<bool> RegisterAsync(RegisterRequest user)
        {
            var isExits =await _userService.GetUserByUsernameAsync(user.Username);
            if (isExits != null)
            {
                return false;
            }
            var userCreated = user.Adapt<User>();
            userCreated.HashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return await _userService.AddUserAsync(userCreated);
        }
    }
}
