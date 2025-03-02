using Microsoft.AspNetCore.Mvc;
using mobile_api.Services.Interface;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/tour")]
    public class TourController: ControllerBase
    {
        private readonly ILogger<TourController> _logger;
        private readonly ITourService _tourService;
        public TourController(ILogger<TourController> logger, ITourService tourService)
        {
            _logger = logger;
            _tourService = tourService;
        }
        [HttpGet("GetTours")]
        public IActionResult GetTours()
        {
            return Ok();
        }
        [HttpGet("GetTourById/{id}")]
        public IActionResult GetTourById(string id)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateTour()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateTour()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteTour()
        {
            return Ok();
        }
    }
}
