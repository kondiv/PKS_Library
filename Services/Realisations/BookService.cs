using Microsoft.EntityFrameworkCore;
using PKS_Library.CustomExceptions;
using PKS_Library.Models;
using PKS_Library.Repositories.Interfaces;
using PKS_Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Services.Realisations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task AddBookAsync(Book book)
        {
            var existingBook = await _bookRepository.GetBookByIsbnAsync(book.Isbn);

            if (existingBook != null)
                throw new AlreadyExistsException("Книга уже существует.");

            await _bookRepository.CreateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            _ = await _bookRepository.GetBookByIdAsync(id) ?? throw new NotFoundException("Книги не существует");

            await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id) ?? throw new NotFoundException("Книги не существует");
        }

        public async Task UpdateBookAsync(Book book)
        {
            ArgumentNullException.ThrowIfNull(book);

            try
            {
                await _bookRepository.UpdateBookAsync(book);
            }

            catch (NotFoundException ex)
            {
                throw new Exception(ex.Message, ex);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message, ex);
            }

            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
