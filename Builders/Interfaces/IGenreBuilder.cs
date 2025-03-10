using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Builders.Interfaces
{
    public interface IGenreBuilder
    {
        IGenreBuilder SetName(string name);

        IGenreBuilder SetDescription(string description);

        Genre Build();
    }
}
