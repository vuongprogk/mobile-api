using System.ComponentModel.DataAnnotations;

namespace mobile_api.Models
{
    public class Tour
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
    }
}
