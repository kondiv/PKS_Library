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
            var book = await GetBookById(id);
            if (book != null)
            {
                _dbContext.Remove<Book>(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthor(Author author)
        {
            return await _dbContext.Books
                .Where(book => book.AuthorId == author.AuthorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenre(Genre genre)
        {
            return await _dbContext.Books
                .Where(book => book.GenreId == genre.GenreId)
                .ToListAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _dbContext.Books.ExecuteUpdateAsync(b => b.SetProperty(b => b.Isbn, book.Isbn)
                                                            .SetProperty(b => b.QuantityInStock, book.QuantityInStock)
                                                            .SetProperty(b => b.Author, book.Author)
                                                            .SetProperty(b => b.PublishYear, book.PublishYear)
                                                            .SetProperty(b => b.AuthorId, book.AuthorId)
                                                            .SetProperty(b => b.Genre, book.Genre)
                                                            .SetProperty(b => b.GenreId, book.GenreId));
            await _dbContext.SaveChangesAsync();
        }
    }
}
