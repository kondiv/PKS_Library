using PKS_Library.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PKS_Library.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<IEnumerable<Book>> GetBooksByAuthor(Author author);
        public Task<IEnumerable<Book>> GetBooksByGenre(Genre genre);
        public Task<Book?> GetBookById(int id);
        public Task CreateBookAsync(Book book);
        public Task UpdateBookAsync(Book book);
        public Task DeleteBookAsync(int id);
    }
}
