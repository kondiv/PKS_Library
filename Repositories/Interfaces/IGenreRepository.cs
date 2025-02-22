using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();

        Task<Genre?> GetGenreByIdAsync(int id);

        Task<Genre?> GetGenreByNameAsync(string name);

        Task CreateGenreAsync(Genre genre);

        Task UpdateGenreAsync(Genre genre);

        Task DeleteGenreAsync(int id);
    }
}
