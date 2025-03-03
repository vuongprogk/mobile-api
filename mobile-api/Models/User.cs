using System.ComponentModel.DataAnnotations;

namespace mobile_api.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string HashPassword { get; set; }
        public string Email { get; set; } = string.Empty;

    }
}
