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
        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        Task<Author?> GetAuthorByIdAsync(int id);

        Task<Author?> GetAuthorByLastNameAsync(string lastName);

        Task<Author?> GetAuthorByFirstAndLastNameAsync(string firstName, string lastName);

        Task CreateAuthorAsync(Author author);

        Task UpdateAuthorAsync(Author author);

        Task DeleteAuthorAsync(int id);
    }
}
