using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<IEnumerable<Author>> GetAllAuthorsAsync();

        public Task<Author> GetAuthorByIdAsync(int id);

        public Task<Author> GetAuthorByLastNameAsync(string lastName);

        Task<Author> GetAuthorByFirstAndLastNameAsync(string firstName, string lastName);

        public Task CreateAuthorAsync(Author author);

        public Task UpdateAuthorAsync(Author author);

        public Task DeleteAuthorAsync(Author author);
    }
}
