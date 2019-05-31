using PalindromeCheck.Interfaces;
using PalindromeCheck.Models;
using System.Collections.Generic;
using System.Linq;

namespace PalindromeCheck.Factory
{
    // Summary:
    //     A factory to produce palindrome strategy instance base on strategy type.
    public class PalindromeFactory : IPalindromeFactory
    {
        readonly IEnumerable<IPalindromeStrategy> palindromeStrategies;

        public PalindromeFactory(IEnumerable<IPalindromeStrategy> palindromeStrategies)
        {
            this.palindromeStrategies = palindromeStrategies;
        }

        // Summary:
        //     Create a strategy instance base on required strategy type.
        //
        // Parameters:
        //   strategyType:
        //     The enum represents the type of target strategy.
        //
        // Returns:
        //     The palindrome strategy instance.
        //
        // Exceptions:
        //   PalindromeStrategyNotImplementedException:
        //     When cannot initialize an instance meets target strategy type.
        public IPalindromeStrategy GetPalindromeStrategy(PalindromeStrategyType strategyType)
        {
            IPalindromeStrategy palindromeStrategy = palindromeStrategies.SingleOrDefault(p => p.StategyType == strategyType);
            if (palindromeStrategy == null) { throw new PalindromeStrategyNotImplementedException(strategyType.ToString()); }

            return palindromeStrategy;
        }
    }
}
