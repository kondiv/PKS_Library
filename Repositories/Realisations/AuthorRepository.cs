using Microsoft.EntityFrameworkCore;
using PKS_Library.CustomExceptions;
using PKS_Library.Models;
using PKS_Library.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Repositories.Realisations;

public class AuthorRepository : IAuthorRepository
{
    private readonly PksBooksContext _dbContext;

    public AuthorRepository(PksBooksContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAuthorAsync(Author author)
    {
        await _dbContext.Authors.AddAsync(author);

        try
        {
            await _dbContext.SaveChangesAsync();
        }

        catch (DbUpdateConcurrencyException ex)
        {
            throw new Exception("Произошел конфликт при изменении книги.", ex);
        }

        catch (DbUpdateException ex)
        {
            throw new Exception("Произошла ошибка при сохранении изменений.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Произошла непредвиденная ошибка: {ex.Message}", ex);
        }
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await GetAuthorByIdAsync(id);

        if (author != null)
        {

            _dbContext.Authors.Remove(author);

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _dbContext.Authors.ToListAsync();
    }

    public async Task<Author?> GetAuthorByFirstAndLastNameAsync(string firstName, string lastName)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(a => a.FirstName == firstName && a.LastName == lastName);
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await _dbContext.Authors.FindAsync(id);
    }

    public async Task<Author?> GetAuthorByLastNameAsync(string lastName)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(a => a.LastName == lastName);
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        ArgumentNullException.ThrowIfNull(author);

        var existingAuthor = await GetAuthorByIdAsync(author.AuthorId) ??
            throw new NotFoundException($"Автор с ID {author.AuthorId} не найден");

        _dbContext.Authors.Entry(existingAuthor).CurrentValues.SetValues(author);

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при сохранении", ex);
        }
    }
}
