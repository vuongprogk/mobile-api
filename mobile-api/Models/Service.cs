using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mobile_api.Models
{
    public class Service
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string TourId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
