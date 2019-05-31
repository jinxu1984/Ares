using PalindromeCheck.Interfaces;
using PalindromeCheck.Models;
using PalindromeCheck.Utilities;

namespace PalindromeCheck.Strategies
{
    // Summary:
    //     A concrete strategy class, which encapsulates the logic to check whether a given string is palindrome.
    //     The main idea is to iterate the given string forward and backward simultaneously, one character at a time. 
    //     If the there is a match the loop continues, otherwise, the loop exits.
    public class RecursivePalindromeStrategy : IPalindromeStrategy
    {
        public PalindromeStrategyType StategyType => PalindromeStrategyType.Recursive;

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

            string cleanedInput = StringUtils.RemovePunctuations(input.ToLower());
            bool isPalindrome = CheckIfInputIsPalindromeRecursively(cleanedInput, 0, cleanedInput.Length - 1);

            return isPalindrome;
        }

        // Summary:
        //     Check whether 2 symmetric charaters in a given string are equal.
        //
        // Parameters:
        //   input:
        //     The given string to check
        //
        //  forward:
        //      Forward charater position
        //
        //  backward:
        //      Backward charater position
        //
        // Returns:
        //     Return true if 2 characters are equal, otherwise return false
        private bool CheckIfInputIsPalindromeRecursively(string input, int forward, int backward)
        {
            if (forward == backward) { return true; }
            if (input[forward] != input[backward]) { return false; }

            if (forward < backward)
            {
                return CheckIfInputIsPalindromeRecursively(input, forward + 1, backward - 1);
            }

            return true;
        }
    }
}
