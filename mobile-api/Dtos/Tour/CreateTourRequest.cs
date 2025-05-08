using System.ComponentModel.DataAnnotations;

namespace mobile_api.Dtos.Tour
{
    public class CreateTourRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        [StringLength(100, ErrorMessage = "Destination cannot exceed 100 characters")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Compare("StartDate", ErrorMessage = "End date must be after start date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }
    }
}
