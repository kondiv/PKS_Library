using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Builders.Interfaces
{
    public interface IAuthorBuilder
    {
        IAuthorBuilder SetFirstName(string firstName);

        IAuthorBuilder SetLastName(string lastName);

        IAuthorBuilder SetBirthdate(DateOnly birthdate);

        IAuthorBuilder SetCountry(string country);

        Author Build();
    }
}
