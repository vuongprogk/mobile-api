using Mapster;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetTours()
        {
            try
            {
                _logger.LogInformation($"{nameof(TourController)} action: {nameof(GetTours)}");
                var tours = await _tourService.GetTours();
                var tourResponses = tours.Adapt<IEnumerable<TourResponse>>();

                var response = new GlobalResponse()
                {
                    Data = tourResponses,
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
        [AllowAnonymous]
        public async Task<IActionResult> GetTourById(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(TourController)} action: {nameof(GetTourById)}");
                var tour = await _tourService.GetTourById(id);
                if (tour == null)
                {
                    return NotFound(new GlobalResponse()
                    {
                        Message = "Tour not found",
                        StatusCode = 404
                    });
                }

                var tourResponse = tour.Adapt<TourResponse>();
                var response = new GlobalResponse()
                {
                    Data = tourResponse,
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTour([FromForm] CreateTourRequest request)
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

                _logger.LogInformation($"{nameof(TourController)} action: {nameof(CreateTour)}");
                if (request.Image == null || request.Image.Length == 0)
                {
                    return BadRequest(new GlobalResponse()
                    {
                        Message = "Image is required",
                        StatusCode = 400
                    });
                }
                // persist image to server
                // create directory if not exists
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                // generate image name
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageName);
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }
                var tour = request.Adapt<Tour>();
                // wrap image link by wwwroot to access from client
                imageName = Path.Combine("images", imageName);
                tour.ImageUrl = imageName;
                var result = await _tourService.CreateTour(tour);

                if (!result)
                {
                    return BadRequest(new GlobalResponse()
                    {
                        Message = "Failed to create tour",
                        StatusCode = 400
                    });
                }

                var tourResponse = tour.Adapt<TourResponse>();
                var response = new GlobalResponse()
                {
                    Data = tourResponse,
                    Message = "Create tour success",
                    StatusCode = 201
                };
                return CreatedAtAction(nameof(GetTourById), new { id = tour.Id }, response);
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

        [HttpPut("UpdateTour/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTour([FromRoute] string id, [FromBody] UpdateTourRequest request)
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
                // check if tour exists
                var tourExists = await _tourService.GetTourById(id);
                var persistImageOld = tourExists.ImageUrl;
                if (tourExists == null)
                {
                    return NotFound(new GlobalResponse()
                    {
                        Message = "Tour not found",
                        StatusCode = 404
                    });
                }
                _logger.LogInformation($"{nameof(TourController)} action: {nameof(UpdateTour)}");
                // check if image is null
                if (request.Image != null && request.Image.Length > 0)
                {
                    // persist image to server
                    // create directory if not exists
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    // generate image name
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                    
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.Image.CopyToAsync(stream);
                    }
                    // wrap image link by wwwroot to access from client
                    imageName = Path.Combine("images", imageName);
                    tourExists.ImageUrl = imageName;
                }
                
                
                
                var result = await _tourService.UpdateTour(tourExists);
                if (!result)
                {
                    return BadRequest(new GlobalResponse()
                    {
                        Message = "Failed to update tour",
                        StatusCode = 400
                    });
                }
                // delete old image
                if (persistImageOld != null)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", persistImageOld);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var tourResponse = tourExists.Adapt<TourResponse>();
                var response = new GlobalResponse()
                {
                    Data = tourResponse,
                    Message = "Update tour success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TourController)} action: {nameof(UpdateTour)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}
