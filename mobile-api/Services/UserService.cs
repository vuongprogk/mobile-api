using mobile_api.Repositories.Interfaces;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class UserService: IUserService
    {
        private ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public UserService(ILogger<UserService> logger, IUserRepository userRepository, ITokenService token)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = token;
        }
    }
}
