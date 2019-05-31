using System;

namespace PalindromeCheck.Models
{
    public class ServiceNotRegisteredException : Exception
    {
        public ServiceNotRegisteredException()
        {
        }

        public ServiceNotRegisteredException(string type)
            : base(String.Format("Service - {0} is not registered in DI container", type))
        {
        }
    }
}
