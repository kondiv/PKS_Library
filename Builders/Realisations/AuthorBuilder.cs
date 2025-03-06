using PKS_Library.Builders.Interfaces;
using PKS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Builders.Realisations
{
    public class AuthorBuilder : IAuthorBuilder
    {
        private Author _author = new();

        public Author Build()
        {
            Author author = _author;
            _author = new();
            return author;
        }

        public IAuthorBuilder SetBirthdate(DateOnly birthdate)
        {
            _author.Birthdate = birthdate;
            return this;
        }

        public IAuthorBuilder SetCountry(string country)
        {
            _author.Country = country;
            return this;
        }

        public IAuthorBuilder SetFirstName(string firstName)
        {
            _author.FirstName = firstName;
            return this;
        }

        public IAuthorBuilder SetLastName(string lastName)
        {
            _author.LastName = lastName;
            return this;
        }
    }
}
