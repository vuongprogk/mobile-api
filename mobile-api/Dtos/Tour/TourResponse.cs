using mobile_api.Models;
using Newtonsoft.Json;

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
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [JsonIgnore] public List<Tag> Tags { get; set; } = new List<Tag>();
        [JsonIgnore] public List<Category> Categories { get; set; } = new List<Category>();
    }
}