using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [ValidateNever]
        public ICollection<Tag> Tags { get; set; }
        [ValidateNever]
        public ICollection<Category> Categories { get; set; }
        [ValidateNever]
        public ICollection<Service> Services { get; set; }
    }
}
