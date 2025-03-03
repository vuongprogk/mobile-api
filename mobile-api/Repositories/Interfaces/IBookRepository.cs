using mobile_api.Models;

namespace mobile_api.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<Book> GetBookByIdAsync(string id);
        public Task<IEnumerable<Book>> GetBooksAsync();
        public Task<IEnumerable<Book>> GetBooksByUserIdAsync(string authorId);
        public Task<bool> AddBookAsync(Book book);
        public Task<bool> UpdateBookAsync(Book book);
        public Task<bool> DeleteBookAsync(string id);
        Task<IEnumerable<Book>> GetBookByUsernameAsync(string username);
    }
}
