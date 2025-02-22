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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;

        public GenreService(IGenreRepository genreRepository)
        {
            _repository = genreRepository;
        }

        public async Task CreateGenreAsync(Genre genre)
        {
            ArgumentNullException.ThrowIfNull(nameof(genre));

            var existingGenre = await _repository.GetGenreByNameAsync(genre.Name);

            if (existingGenre != null)
            {
                throw new AlreadyExistsException("Жанр уже существует");
            }

            await _repository.CreateGenreAsync(genre);
        }

        public async Task DeleteGenreAsync(int id)
        {
            if (id < 0)
                throw new WrongArgumentProvidedException("ID не может быть меньше или равен нулю");

            await _repository.DeleteGenreAsync(id);
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _repository.GetAllGenresAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            if (id < 0)
                throw new WrongArgumentProvidedException($"ID не может быть меньше или равен нулю");

            return await _repository.GetGenreByIdAsync(id) ?? throw new NotFoundException($"Жанра с ID {id} не существует");
        }

        public async Task<Genre> GetGenreByNameAsync(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new WrongArgumentProvidedException("Название жанра не может быть пустым");

            return await _repository.GetGenreByNameAsync(name) ?? throw new NotFoundException($"Жанр с названием {name} не найден");
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            if (genre == null)
                throw new WrongArgumentProvidedException();

            await _repository.UpdateGenreAsync(genre);
        }
    }
}
