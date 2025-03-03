using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobile_api.Dtos.Book;
using mobile_api.Models;
using mobile_api.Responses;
using mobile_api.Services.Interface;
using System.Threading.Tasks;

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
                var response = new GlobalResponse()
                {
                    Data = await _bookService.GetBooks(),
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
        public async Task<IActionResult> GetBookByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookController)} action: {nameof(GetBookByIdAsync)}");
                var response = new GlobalResponse()
                {
                    Data = await _bookService.GetBookById(id),
                    Message = "Get book by id success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BookController)} action: {nameof(GetBookByIdAsync)} error");
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
                _logger.LogInformation($"{nameof(BookController)} action: {nameof(CreateBook)}");
                var book = request.Adapt<Book>();
                var response = new GlobalResponse()
                {
                    Data = await _bookService.CreateBook(book),
                    Message = "Create book success",
                    StatusCode = 200
                };
                return new JsonResult(response);
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
