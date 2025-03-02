using System.ComponentModel.DataAnnotations;

namespace mobile_api.Models
{
    public class Ticket
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Title { get; set; }
        public string AgeGroup { get; set; }
    }
}
