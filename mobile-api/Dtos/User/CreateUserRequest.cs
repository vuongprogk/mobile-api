﻿using mobile_api.Models;

namespace mobile_api.Dtos.User
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } = Role.User;
    }
}
