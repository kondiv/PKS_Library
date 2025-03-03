using PKS_Library.Builders.Interfaces;
using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Builders.Realisations
{
    public class BookBuilder : IBookBuilder
    {
        private Book _book = new();

        public Book Build()
        {
            Book book = _book;
            _book = new();
            return book;
        }

        public IBookBuilder SetAuthor(Author author)
        {
            _book.Author = author;
            return this;
        }

        public IBookBuilder SetGenre(Genre genre)
        {
            _book.Genre = genre;
            return this;
        }

        public IBookBuilder SetIsbn(string isbn)
        {
            _book.Isbn = isbn;
            return this;
        }

        public IBookBuilder SetPublishYear(int publishYear)
        {
            _book.PublishYear = publishYear;
            return this;
        }

        public IBookBuilder SetQuantityInStock(int quantityInStock)
        {
            _book.QuantityInStock = quantityInStock;
            return this;
        }

        public IBookBuilder SetTitle(string title)
        {
            _book.Title = title;
            return this;
        }
    }
}
