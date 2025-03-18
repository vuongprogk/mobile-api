using mobile_api.Models;

namespace mobile_api.Dtos.User
{
    public class UpdateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
