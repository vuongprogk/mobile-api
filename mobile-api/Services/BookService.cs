using Microsoft.EntityFrameworkCore;
using mobile_api.Data;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;
using mobile_api.Services.Interface;

namespace mobile_api.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _book;
        private readonly ILogger<BookService> _logger;
        private readonly ApplicationDbContext _context;
        public BookService(IBookRepository bookRepository, ILogger<BookService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _book = bookRepository;
            _context = context;
        }
        public async Task<bool> CreateBook(Book book)
        {
            if (book == null)
            {
                return false;
            }
            //get tour info
            var tour = await _context.Tours.Include(item => item.Services).FirstOrDefaultAsync(item => item.Id == book.TourId);
            if (tour == null)
            {
                return false;
            }
            //get service info
            var totalPrice = tour.Price;
            // loop through service of tour and adding price 
            foreach (var service in tour.Services)
            {
                var serviceInfo = await _context.Services.FirstOrDefaultAsync(item => item.Id == service.Id);
                if (serviceInfo == null)
                {
                    return false;
                }
                totalPrice += serviceInfo.Price;
            }
            book.TotalPrice = totalPrice * book.Quantity;
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

        public async Task<IEnumerable<BookResponse>> GetBooksWithDetails()
        {
            _logger.LogInformation($"{nameof(BookService)} action: {nameof(GetBooksWithDetails)}");
            return await _book.GetBooksWithDetailsAsync();
        }
    }
}
