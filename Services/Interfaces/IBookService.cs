using PKS_Library.Data;
using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Services.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAllBooksAsync();

        public Task<IEnumerable<Book>> GetBooksAsync(Author? authorFilter = null, Genre? genreFilter = null, BookSortBy bookSortBy = BookSortBy.None);

        public Task<Book> GetBookByIdAsync(int id);

        public Task AddBookAsync(Book book);

        public Task UpdateBookAsync(Book book);

        public Task DeleteBookAsync(int id);
    }
}
