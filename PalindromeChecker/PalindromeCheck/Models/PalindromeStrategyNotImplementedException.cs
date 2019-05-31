using System;
using System.Collections.Generic;
using System.Text;

namespace PalindromeCheck.Models
{
    public class PalindromeStrategyNotImplementedException : Exception
    {
        public PalindromeStrategyNotImplementedException()
        {
        }

        public PalindromeStrategyNotImplementedException(string strategyName)
            : base(String.Format("Palindrome Strategy - {0} is not implemented", strategyName))
        {
        }
    }
}
