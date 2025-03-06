using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Data
{
    public record AuthorValidationRequest(string FirstName, string LastName, string Birthdate, string Country);
}
