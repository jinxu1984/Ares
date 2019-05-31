using System;
using System.Collections.Generic;
using System.Text;

namespace Caching
{
    public class InvalidCacheSizeException : Exception
    {
        public InvalidCacheSizeException()
        {
        }

        public InvalidCacheSizeException(string message) : base(message)
        {
        }

        public InvalidCacheSizeException(string message, Exception e) : base(message, e)
        {
        }
    }
}
