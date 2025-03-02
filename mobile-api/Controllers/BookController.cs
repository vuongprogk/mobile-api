using Microsoft.AspNetCore.Mvc;
using mobile_api.Services.Interface;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController: ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet("GetBooks")]
        public IActionResult GetBooks()
        {
            return Ok();
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetBookById(string id)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateBook()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateBook()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteBook()
        {
            return Ok();
        }
    }
}
