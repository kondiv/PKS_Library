using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Builders.Interfaces
{
    public interface IBookBuilder
    {
        IBookBuilder SetTitle(string title);
        IBookBuilder SetAuthor(Author author);
        IBookBuilder SetGenre(Genre genre);
        IBookBuilder SetIsbn(string isbn);
        IBookBuilder SetPublishYear(int publishYear);
        IBookBuilder SetQuantityInStock(int quantityInStock);
        Book Build();
    }
}
