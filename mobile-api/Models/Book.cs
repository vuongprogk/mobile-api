using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mobile_api.Models
{
    public class Book
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string TourId { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("TourId")]
        public Tour Tour { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal TotalPrice { get; set; }
        
    }
}
