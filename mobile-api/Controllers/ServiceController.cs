using Microsoft.AspNetCore.Mvc;
using mobile_api.Responses;
using mobile_api.Services.Interface;
using System.Threading.Tasks;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/service")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IServiceService _serviceService;
        public ServiceController(ILogger<ServiceController> logger, IServiceService serviceService)
        {
            _logger = logger;
            _serviceService = serviceService;
        }
        [HttpGet("GetServices")]
        public async Task<IActionResult> GetServices()
        {
            try
            {
                _logger.LogInformation($"{nameof(ServiceController)} action: {nameof(GetServices)}");
                var response = new GlobalResponse()
                {
                    Data = await _serviceService.GetServices(),
                    Message = "Get services success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ServiceController)} action: {nameof(GetServices)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
        [HttpGet("GetServiceById/{id}")]
        public async Task<IActionResult> GetServiceById(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(ServiceController)} action: {nameof(GetServiceById)}");
                var response = new GlobalResponse()
                {
                    Data = await _serviceService.GetServiceById(id),
                    Message = "Get service by id success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ServiceController)} action: {nameof(GetServiceById)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
        [HttpGet("GetServiceByTourId/{id}")]
        public async Task<IActionResult> GetServiceByTourId(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(ServiceController)} action: {nameof(GetServiceByTourId)}");
                var response = new GlobalResponse()
                {
                    Data = await _serviceService.GetServiceByTourId(id),
                    Message = "Get service by tour id success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ServiceController)} action: {nameof(GetServiceByTourId)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}
