using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.CustomExceptions
{
    public class PageDoesNotExistException : Exception
    {
        public PageDoesNotExistException()
            : base()
        {
            
        }

        public PageDoesNotExistException(string message)
            : base(message) 
        {
            
        }
    }
}
