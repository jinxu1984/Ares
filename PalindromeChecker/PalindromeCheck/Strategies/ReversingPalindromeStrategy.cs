using PalindromeCheck.Interfaces;
using PalindromeCheck.Models;
using PalindromeCheck.Utilities;
using System;

namespace PalindromeCheck.Strategies
{
    // Summary:
    //     A concrete strategy class, which encapsulates the logic to check whether a given string is palindrome.
    //     The main idea is to reverse the given string, then compare original string and reversed string.
    public class ReversingPalindromeStrategy : IPalindromeStrategy
    {
        public PalindromeStrategyType StategyType => PalindromeStrategyType.Reversing;

        // Summary:
        //     Check whether input is palindrome
        //
        // Parameters:
        //   input:
        //     The given string to check
        //
        // Returns:
        //     Retrun true if input is palindrome, otherwise return false
        public bool CheckIfInputIsPalindrome(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { return false; }

            string cleanedInput = StringUtils.RemovePunctuations(input);
            string reversedInput = Reverse(cleanedInput);
            bool isPalindrome = cleanedInput.Equals(reversedInput, StringComparison.OrdinalIgnoreCase);

            return isPalindrome;
        }

        private string Reverse(string input)
        {
            char[] inputAarry = input.ToCharArray();

            Array.Reverse(inputAarry);
            string reversedInput = new String(inputAarry);

            return reversedInput;
        }
    }
}
