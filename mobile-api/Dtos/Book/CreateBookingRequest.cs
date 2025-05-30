﻿using System.ComponentModel.DataAnnotations;

namespace mobile_api.Dtos.Book
{
    public class CreateBookingRequest
    {
        [Required(ErrorMessage = "Tour ID is required")]
        public string TourId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Booking date is required")]
        public DateTime BookingDate { get; set; }
    }
}
