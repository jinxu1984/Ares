using PalindromeCheck.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PalindromeCheck.Interfaces
{
    // Summary:
    //     Exposes the function which to check whether a given string is palindrome.
    public interface IPalindromeStrategy
    {
        PalindromeStrategyType StategyType { get; }
        bool CheckIfInputIsPalindrome(string input);
    }
}
