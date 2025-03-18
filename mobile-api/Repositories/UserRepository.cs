using Microsoft.EntityFrameworkCore;
using mobile_api.Data;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
       
        public async Task<bool> AddNewUserAsync(User user)
        {
            _logger.LogInformation($"{nameof(UserRepository)} action: {nameof(AddNewUserAsync)}");
            var result = await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            _logger.LogInformation($"{nameof(UserRepository)} action: {nameof(GetUserByNameAsync)}");
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            _logger.LogInformation($"{nameof(UserRepository)} action: {nameof(GetUsers)}");
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(Role role)
        {
            _logger.LogInformation($"{nameof(UserRepository)} action: {nameof(GetUsersByRoleAsync)}");
            return await _context.Users.Where(x => x.Role == role).ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User user, string id)
        {
            _logger.LogInformation($"{nameof(UserRepository)} action: {nameof(UpdateUserAsync)}");
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userToUpdate == null)
            {
                return false;
            }
            userToUpdate.Username = user.Username;
            userToUpdate.HashPassword = user.HashPassword;
            userToUpdate.Email = user.Email;
            userToUpdate.Role = user.Role;
            _context.Users.Update(userToUpdate);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
