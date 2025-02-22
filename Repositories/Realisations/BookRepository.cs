using PKS_Library.Models;
using PKS_Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Repositories.Realisations
{
    public class BookRepository : IBookRepository
    {
        private readonly PksBooksContext _dbContext;

        public BookRepository(PksBooksContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateBookAsync(Book book)
        {
            await _dbContext.AddAsync(book);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);

            if(book != null)
            {
                _dbContext.Books.Remove(book);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<Book?> GetBookByIsbnAsync(string isbn)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.Isbn == isbn);
        }

        public async Task UpdateBookAsync(Book book)
        {
            _dbContext.Books.Update(book);

            await _dbContext.SaveChangesAsync();
        }
    }
}
