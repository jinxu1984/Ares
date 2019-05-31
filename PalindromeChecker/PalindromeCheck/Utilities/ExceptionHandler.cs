using PalindromeCheck.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PalindromeCheck.Utilities
{
    // Summary:
    //     Exposes utility functions for application eror handling
    public class ExceptionHandler
    {
        public static void TryCatch(Action action, Action<Exception> actionToLogException)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                actionToLogException(exception);
            }
        }
    }
}
