using System.ComponentModel.DataAnnotations;

namespace mobile_api.Dtos.Service
{
    public class CreateServiceRequest
    {
        [Required(ErrorMessage = "Tour ID is required")]
        public string TourId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }
    }
} 