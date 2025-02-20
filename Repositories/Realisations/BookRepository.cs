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

        public async Task DeleteBookAsync(Book book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _dbContext.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<Book?> GetBookByIsbnAsync(string isbn)
        {
            return await _dbContext.Books.AsNoTracking()
                                         .FirstOrDefaultAsync(b => b.Isbn == isbn);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Author author)
        {
            return await _dbContext.Books
                .Where(book => book.AuthorId == author.AuthorId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(Genre genre)
        {
            return await _dbContext.Books
                .Where(book => book.GenreId == genre.GenreId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null");
            }

            var existingBook = await GetBookByIdAsync(book.BookId) ?? throw new KeyNotFoundException($"Book with ID {book.BookId} not found");

            try
            {
                _dbContext.Entry(existingBook).CurrentValues.SetValues(book);
                await _dbContext.SaveChangesAsync();
            }

            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the book", ex);
            }
        }
    }
}
