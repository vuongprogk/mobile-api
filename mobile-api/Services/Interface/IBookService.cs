using mobile_api.Models;

namespace mobile_api.Services.Interface
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetBooks();
        public Task<Book> GetBookById(string id);
        public Task<bool> CreateBook(Book book);
        public Task<IEnumerable<Book>> GetBookByUsername(string username);
        public Task<IEnumerable<Book>> GetBooksByUserId(string userId);
        public Task<IEnumerable<BookResponse>> GetBooksWithDetails();
    }
}
