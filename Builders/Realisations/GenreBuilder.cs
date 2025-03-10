using PKS_Library.Builders.Interfaces;
using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Builders.Realisations;

public class GenreBuilder : IGenreBuilder
{
    private Genre _genre = new();

    public Genre Build()
    {
        var genre = _genre;
        _genre = new();
        return genre;
    }

    public IGenreBuilder SetDescription(string description)
    {
        _genre.Description = description;
        return this;
    }

    public IGenreBuilder SetName(string name)
    {
        _genre.Name = name;
        return this;
    }
}
