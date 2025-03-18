namespace mobile_api.Dtos.Tour
{
    public class TourResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
} 