using Mapster;
using Microsoft.AspNetCore.Mvc;
using mobile_api.Dtos.Tour;
using mobile_api.Models;
using mobile_api.Responses;
using mobile_api.Services.Interface;
using System.Threading.Tasks;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/tour")]
    public class TourController : ControllerBase
    {
        private readonly ILogger<TourController> _logger;
        private readonly ITourService _tourService;
        public TourController(ILogger<TourController> logger, ITourService tourService)
        {
            _logger = logger;
            _tourService = tourService;
        }
        [HttpGet("GetTours")]
        public async Task<IActionResult> GetTours()
        {
            try
            {
                _logger.LogInformation($"{nameof(TourController)} action: {nameof(GetTours)}");
                var response = new GlobalResponse()
                {
                    Data = await _tourService.GetTours(),
                    Message = "Get tours success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TourController)} action: {nameof(GetTours)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
        [HttpGet("GetTourById/{id}")]
        public async Task<IActionResult> GetTourById(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(TourController)} action: {nameof(GetTourById)}");
                var response = new GlobalResponse()
                {
                    Data = await _tourService.GetTourById(id),
                    Message = "Get tour by id success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TourController)} action: {nameof(GetTourById)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
        [HttpPost("CreateTour")]
        public async Task<IActionResult> CreateTour([FromBody] CreateTourRequest request)
        {
            try
            {
                _logger.LogInformation($"{nameof(TourController)} action: {nameof(CreateTour)}");
                var tour = request.Adapt<Tour>();
                var response = new GlobalResponse()
                {
                    Data = await _tourService.CreateTour(tour),
                    Message = "Create tour success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TourController)} action: {nameof(CreateTour)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}
