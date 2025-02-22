using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.CustomExceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException()
            : base()
        {
            
        }

        public AlreadyExistsException(string message)
            : base(message)
        {
            
        }
    }
}
