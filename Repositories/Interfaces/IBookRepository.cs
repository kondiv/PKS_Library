using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PKS_Library.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PKS_Library.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooksAsync();

        public Task<Book?> GetBookByIdAsync(int id);

        public Task<Book?> GetBookByIsbnAsync(string isbn);

        public Task CreateBookAsync(Book book);

        public Task UpdateBookAsync(Book book);

        public Task DeleteBookAsync(int id);
    }
}
