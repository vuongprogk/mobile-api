using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobile_api.Dtos.Service;
using mobile_api.Models;
using mobile_api.Responses;
using mobile_api.Services.Interface;
using System.Threading.Tasks;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/service")]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetServices()
        {
            try
            {
                _logger.LogInformation($"{nameof(ServiceController)} action: {nameof(GetServices)}");
                var services = await _serviceService.GetServices();
                var serviceResponses = services.Adapt<IEnumerable<ServiceResponse>>();
                
                var response = new GlobalResponse()
                {
                    Data = serviceResponses,
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
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceById(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(ServiceController)} action: {nameof(GetServiceById)}");
                var service = await _serviceService.GetServiceById(id);
                if (service == null)
                {
                    return NotFound(new GlobalResponse()
                    {
                        Message = "Service not found",
                        StatusCode = 404
                    });
                }

                var serviceResponse = service.Adapt<ServiceResponse>();
                var response = new GlobalResponse()
                {
                    Data = serviceResponse,
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
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceByTourId(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(ServiceController)} action: {nameof(GetServiceByTourId)}");
                var services = await _serviceService.GetServiceByTourId(id);
                var serviceResponses = services.Adapt<IEnumerable<ServiceResponse>>();
                
                var response = new GlobalResponse()
                {
                    Data = serviceResponses,
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

        [HttpPost("CreateService")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new GlobalResponse()
                    {
                        Message = "Invalid request data",
                        StatusCode = 400,
                        Data = ModelState.Values.SelectMany(v => v.Errors)
                    });
                }

                _logger.LogInformation($"{nameof(ServiceController)} action: {nameof(CreateService)}");
                var service = request.Adapt<Service>();
                var result = await _serviceService.CreateService(service);

                if (!result)
                {
                    return BadRequest(new GlobalResponse()
                    {
                        Message = "Failed to create service",
                        StatusCode = 400
                    });
                }

                var serviceResponse = service.Adapt<ServiceResponse>();
                var response = new GlobalResponse()
                {
                    Data = serviceResponse,
                    Message = "Create service success",
                    StatusCode = 201
                };
                return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ServiceController)} action: {nameof(CreateService)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }

        [HttpPut("UpdateService/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateService([FromRoute] string id, [FromBody] UpdateServiceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new GlobalResponse()
                    {
                        Message = "Invalid request data",
                        StatusCode = 400,
                        Data = ModelState.Values.SelectMany(v => v.Errors)
                    });
                }

                _logger.LogInformation($"{nameof(ServiceController)} action: {nameof(UpdateService)}");
                var service = request.Adapt<Service>();
                service.Id = id;

                var result = await _serviceService.UpdateService(service);
                if (!result)
                {
                    return NotFound(new GlobalResponse()
                    {
                        Message = "Service not found",
                        StatusCode = 404
                    });
                }

                var serviceResponse = service.Adapt<ServiceResponse>();
                var response = new GlobalResponse()
                {
                    Data = serviceResponse,
                    Message = "Update service success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ServiceController)} action: {nameof(UpdateService)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}
