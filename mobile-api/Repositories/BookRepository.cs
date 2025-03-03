using Microsoft.EntityFrameworkCore;
using mobile_api.Data;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;
        private ILogger<BookRepository> _logger;
        public BookRepository(ApplicationDbContext context, ILogger<BookRepository> logger)
        {
            _db = context;
            _logger = logger;
        }
        public async Task<bool> AddBookAsync(Book book)
        {
            _logger.LogInformation($"{nameof(BookRepository)} action: {nameof(AddBookAsync)}");
            _db.Add(book);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBookAsync(string id)
        {
            _logger.LogInformation($"{nameof(BookRepository)} action: {nameof(DeleteBookAsync)}");
            var book = await _db.Bookings.FirstOrDefaultAsync(item => item.Id == id);
            if (book == null)
            {
                return false;
            }
            _db.Bookings.Remove(book);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<Book> GetBookByIdAsync(string id)
        {
            _logger.LogInformation($"{nameof(BookRepository)} action: {nameof(GetBookByIdAsync)}");
            return await _db.Bookings.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBookByUsernameAsync(string username)
        {
            _logger.LogInformation($"{nameof(BookRepository)} action: {nameof(GetBookByUsernameAsync)}");
            var userId = await _db.Users.FirstOrDefaultAsync(item => item.Username == username);
            if (userId == null)
            {
                return null;
            }
            return await _db.Bookings.Where(item => item.UserId == userId.Id).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            _logger.LogInformation($"{nameof(BookRepository)} action: {nameof(GetBooksAsync)}");
            return await _db.Bookings.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByUserIdAsync(string authorId)
        {
            _logger.LogInformation($"{nameof(BookRepository)} action: {nameof(GetBooksByUserIdAsync)}");
            return await _db.Bookings.Where(item => item.UserId == authorId).ToListAsync();
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            _logger.LogInformation($"{nameof(BookRepository)} action: {nameof(UpdateBookAsync)}");
            _db.Update(book);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
