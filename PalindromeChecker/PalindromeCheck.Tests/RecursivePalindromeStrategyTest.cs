using PalindromeCheck.Strategies;
using Xunit;

namespace PalindromeCheck.Tests
{
    public class RecursivePalindromeStrategyTest
    {
        [Fact]
        public void ReturnTrueWhenInputIsPalindrome()
        {
            RecursivePalindromeStrategy strategy = new RecursivePalindromeStrategy();
            string input = "Madam";

            bool isInputPalindrome = strategy.CheckIfInputIsPalindrome(input);

            Assert.True(isInputPalindrome);
        }

        [Fact]
        public void ReturnTrueWhenInputIsPalindromeAndContainsPunctuations()
        {
            RecursivePalindromeStrategy strategy = new RecursivePalindromeStrategy();
            string input = "to!p sp#ot";

            bool isInputPalindrome = strategy.CheckIfInputIsPalindrome(input);

            Assert.True(isInputPalindrome);
        }

        [Fact]
        public void ReturnFalseWhenInputIsNull()
        {
            RecursivePalindromeStrategy strategy = new RecursivePalindromeStrategy();
            string input = "";

            bool isInputPalindrome = strategy.CheckIfInputIsPalindrome(input);

            Assert.False(isInputPalindrome);
        }

        [Fact]
        public void ReturnFalseWhenInputIsNotPalindrome()
        {
            RecursivePalindromeStrategy strategy = new RecursivePalindromeStrategy();
            string input = "ab";

            bool isInputPalindrome = strategy.CheckIfInputIsPalindrome(input);

            Assert.False(isInputPalindrome);
        }
    }
}
