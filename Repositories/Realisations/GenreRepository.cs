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
    public class GenreRepository : IGenreRepository
    {
        private readonly PksBooksContext _context;

        public GenreRepository(PksBooksContext context)
        {
            _context = context;
        }

        public async Task CreateGenreAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await GetGenreByIdAsync(id);

            if (genre != null)
            {
                _context.Genres.Remove(genre);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<Genre?> GetGenreByNameAsync(string name)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Name == name);
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            _context.Genres.Update(genre);

            await _context.SaveChangesAsync();
        }
    }
}
