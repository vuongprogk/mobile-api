﻿using mobile_api.Models;

namespace mobile_api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByNameAsync(string username);
        public Task<bool> AddNewUserAsync(User user);
        public Task<bool> UpdateUserAsync(User user, string id);
        public Task<IEnumerable<User>> GetUsers();
        public Task<IEnumerable<User>> GetUsersByRoleAsync(Role role);
    }
}
