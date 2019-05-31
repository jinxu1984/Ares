using PalindromeCheck.Models;

namespace PalindromeCheck.Interfaces
{
    // Summary:
    //     Exposes a factory, which can produce palndorme strategy.
    public interface IPalindromeFactory
    {
        IPalindromeStrategy GetPalindromeStrategy(PalindromeStrategyType strategyType);
    }
}
