using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        Task<Author> GetAuthorByLastNameAsync(string lastName);

        Task<Author> GetAuthorByFirstAndLastNameAsync(string firstName, string lastName);

        Task<Author> GetAuthorByIdAsync(int id);

        Task AddAuthorAsync(Author author);

        Task UpdateAuthorAsync(Author author);

        Task DeleteAuthorAsync(int id);
    }
}
