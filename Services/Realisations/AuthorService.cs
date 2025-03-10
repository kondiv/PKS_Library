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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAuthorAsync(Author author)
        {
            ArgumentNullException.ThrowIfNull(author);

            var existingAuthor =  await _repository.GetAuthorByFirstAndLastNameAsync(author.FirstName, author.LastName);

            if(existingAuthor != null 
                && existingAuthor.Birthdate == author.Birthdate 
                && existingAuthor.Country == author.Country)
            {
                throw new AlreadyExistsException("Данный автор уже существует");
            }

            try
            {
                await _repository.AddAuthorAsync(author);
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

        public async Task DeleteAuthorAsync(int id)
        {
            _ = await _repository.GetAuthorByIdAsync(id) ?? throw new NotFoundException("Данного автора не существует");

            await _repository.DeleteAuthorAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _repository.GetAllAuthorsAsync();
        }

        public async Task<Author> GetAuthorByFirstAndLastNameAsync(string firstName, string lastName)
        {
            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName))
                throw new WrongArgumentProvidedException("Поля имя и фамилия не могут быть пустыми");

            var author = await _repository.GetAuthorByFirstAndLastNameAsync(firstName, lastName) ?? 
                throw new NotFoundException("Автор с данными именем и фамилией не найден");

            return author;
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            if (id < 0)
                throw new WrongArgumentProvidedException("Поле ID не может быть отрицательным или ровняться 0");

            var author = await _repository.GetAuthorByIdAsync(id) ??
                throw new NotFoundException($"Автор с ID {id} не найден");

            return author;
        }

        public async Task<Author> GetAuthorByLastNameAsync(string lastName)
        {
            if (String.IsNullOrEmpty(lastName))
                throw new WrongArgumentProvidedException("Поле фамилия не может быть пустой");

            var author = await _repository.GetAuthorByLastNameAsync(lastName) ??
                throw new NotFoundException($"Автора с фамилией {lastName} не существует");

            return author;
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            if (author == null)
                throw new WrongArgumentProvidedException("Аргумент не должен быть null");

            try 
            {
                await _repository.UpdateAuthorAsync(author);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
