using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Infrastructure.Exceptions
{
    public class InvalidInputException: Exception
    {
        public InvalidInputException()
        { }

        public InvalidInputException(string message)
            : base(message)
        { }

        public InvalidInputException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
