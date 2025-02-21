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

        public async Task CreateAuthorAsync(Author author)
        {
            ArgumentNullException.ThrowIfNull(author);
            try
            {
                var existingAuthor = _repository.GetAuthorByFirstAndLastNameAsync(author.FirstName, author.LastName);
            }
            catch (Exception)
            {
                await _repository.CreateAuthorAsync(author);
            }
        }

        public Task DeleteAuthorAsync(Author author)
        {
        }

        public Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorByLastNameAsync(string lastName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
