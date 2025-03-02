using Microsoft.AspNetCore.Mvc;
using mobile_api.Services.Interface;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/service")]
    public class ServiceController: ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IServiceService _serviceService;
        public ServiceController(ILogger<ServiceController> logger, IServiceService serviceService)
        {
            _logger = logger;
            _serviceService = serviceService;
        }
        [HttpGet("GetServices")]
        public IActionResult GetServices()
        {
            return Ok();
        }
        [HttpGet("GetServiceById/{id}")]
        public IActionResult GetServiceById(string id)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateService()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateService()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteService()
        {
            return Ok();
        }
    }
}
