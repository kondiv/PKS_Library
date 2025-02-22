using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.CustomExceptions
{
    internal class WrongArgumentProvidedException : Exception
    {
        public WrongArgumentProvidedException()
            : base()
        {
            
        }

        public WrongArgumentProvidedException(string message)
            : base(message) 
        {
            
        }
    }
}
