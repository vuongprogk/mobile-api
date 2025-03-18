using mobile_api.Models;
using mobile_api.Repositories.Interfaces;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _book;
        private readonly ILogger<BookService> _logger;
        public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
        {
            _logger = logger;
            _book = bookRepository;
        }
        public async Task<bool> CreateBook(Book book)
        {
            _logger.LogInformation($"{nameof(BookService)} action: {nameof(CreateBook)}");
            return await _book.AddBookAsync(book);
        }

        public async Task<Book> GetBookById(string id)
        {
            _logger.LogInformation($"{nameof(BookService)} action: {nameof(GetBookById)}");
            return await _book.GetBookByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBookByUsername(string username)
        {
            _logger.LogInformation($"{nameof(BookService)} action: {nameof(GetBookByUsername)}");
            return await _book.GetBookByUsernameAsync(username);
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            _logger.LogInformation($"{nameof(BookService)} action: {nameof(GetBooks)}");
            return await _book.GetBooksAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByUserId(string userId)
        {
            _logger.LogInformation($"{nameof(BookService)} action: {nameof(GetBooksByUserId)}");
            return await _book.GetBooksByUserIdAsync(userId);
        }
    }
}
