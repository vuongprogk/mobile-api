using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobile_api.Dtos.Book;
using mobile_api.Models;
using mobile_api.Responses;
using mobile_api.Services.Interface;
using System.Threading.Tasks;
using System.Security.Claims;

namespace mobile_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                _logger.LogInformation($"{nameof(BookController)} action: {nameof(GetBooks)}");
                var isAdmin = User.IsInRole("Admin");
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                var response = new GlobalResponse()
                {
                    Data = isAdmin ? await _bookService.GetBooks() : await _bookService.GetBooksByUserId(userId),
                    Message = "Get books success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BookController)} action: {nameof(GetBooks)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetBookById(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookController)} action: {nameof(GetBookById)}");
                var isAdmin = User.IsInRole("Admin");
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var book = await _bookService.GetBookById(id);
                
                if (book == null)
                {
                    return NotFound(new GlobalResponse()
                    {
                        Message = "Booking not found",
                        StatusCode = 404
                    });
                }

                if (!isAdmin && book.UserId != userId)
                {
                    return Forbid();
                }

                var response = new GlobalResponse()
                {
                    Data = book,
                    Message = "Get book by id success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BookController)} action: {nameof(GetBookById)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookingRequest request)
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

                _logger.LogInformation($"{nameof(BookController)} action: {nameof(CreateBook)}");
                
                // Get user ID from token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogError("User ID not found in token");
                    return Unauthorized(new GlobalResponse()
                    {
                        Message = "User ID not found in token",
                        StatusCode = 401
                    });
                }

                _logger.LogInformation($"Creating booking for user ID: {userId}");

                // Create the booking
                var book = new Book
                {
                    TourId = request.TourId,
                    UserId = userId,
                    Quantity = request.Quantity,
                    BookingDate = DateTime.UtcNow,
                    Status = request.Status
                };

                var result = await _bookService.CreateBook(book);
                if (!result)
                {
                    return BadRequest(new GlobalResponse()
                    {
                        Message = "Failed to create booking",
                        StatusCode = 400
                    });
                }

                var response = new GlobalResponse()
                {
                    Data = book,
                    Message = "Create booking success",
                    StatusCode = 201
                };

                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BookController)} action: {nameof(CreateBook)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }

        [HttpGet("GetBookByUsername/{username}")]
        public async Task<IActionResult> GetBookByUsername(string username)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookController)} action: {nameof(GetBookByUsername)}");
                var isAdmin = User.IsInRole("Admin");
                var currentUsername = User.FindFirst(ClaimTypes.Name)?.Value;
                
                if (!isAdmin && currentUsername != username)
                {
                    return Forbid();
                }

                var response = new GlobalResponse()
                {
                    Data = await _bookService.GetBookByUsername(username),
                    Message = "Get book by username success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BookController)} action: {nameof(GetBookByUsername)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}
