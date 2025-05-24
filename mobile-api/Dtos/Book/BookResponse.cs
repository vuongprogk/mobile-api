using System;

namespace mobile_api.Models
{
    public class BookResponse
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string TourId { get; set; }
        public string TourName { get; set; }
        public DateTime BookingDate { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
