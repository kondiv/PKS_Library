using Microsoft.EntityFrameworkCore;
using PKS_Library.Models;
using PKS_Library.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Repositories.Realisations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly PksBooksContext _dbContext;

        public AuthorRepository(PksBooksContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAuthorAsync(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(Author author)
        {
            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByFirstAndLastNameAsync(string firstName, string lastName)
        {
            return await _dbContext.Authors.FirstOrDefaultAsync(a => a.FirstName == firstName && a.LastName == lastName)
                ?? throw new KeyNotFoundException();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _dbContext.Authors.FindAsync(id) ?? throw new KeyNotFoundException();
        }

        public async Task<Author> GetAuthorByLastNameAsync(string lastName)
        {
            return await _dbContext.Authors.FirstOrDefaultAsync(a => a.LastName == lastName) ?? throw new KeyNotFoundException();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();
        }
    }
}
