using mobile_api.Models;
using mobile_api.Repositories.Interfaces;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class UserService: IUserService
    {
        private ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            _logger.LogInformation($"{nameof(UserService)} action: {nameof(GetUserByUsernameAsync)}");
            return await _userRepository.GetUserByNameAsync(username);
        }

        public async Task<bool> AddUserAsync(User user)
        {
            _logger.LogInformation($"{nameof(UserService)} action: {nameof(AddUserAsync)}");
            return await _userRepository.AddNewUserAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            _logger.LogInformation($"{nameof(UserService)} action: {nameof(GetUsers)}");
            return await _userRepository.GetUsers();
        }

        public async Task<bool> UpdateUser(User user, string id)
        {
            _logger.LogInformation($"{nameof(UserService)} action: {nameof(UpdateUser)}");
            return await _userRepository.UpdateUserAsync(user, id);
        }

        public async Task<bool> CreateUser(User user)
        {
            _logger.LogInformation($"{nameof(UserService)} action: {nameof(CreateUser)}");
            return await _userRepository.AddNewUserAsync(user);
        }

        public async Task<User> GetUserById(string id)
        {
            _logger.LogInformation($"{nameof(UserService)} action: {nameof(GetUserById)}");
            return await _userRepository.GetUserByNameAsync(id);
        }
    }
}
