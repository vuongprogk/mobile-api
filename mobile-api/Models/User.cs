using System.ComponentModel.DataAnnotations;

namespace mobile_api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
