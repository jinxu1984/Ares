using PalindromeCheck.Strategies;
using Xunit;

namespace PalindromeCheck.Tests
{
    public class ReversingPalindromeStrategyTest
    {
        [Fact]
        public void ReturnTrueWhenInputIsPalindrome()
        {
            ReversingPalindromeStrategy strategy = new ReversingPalindromeStrategy();
            string input = "Madam";

            bool isInputPalindrome = strategy.CheckIfInputIsPalindrome(input);

            Assert.True(isInputPalindrome);
        }

        [Fact]
        public void ReturnTrueWhenInputIsPalindromeAndContainsPunctuations()
        {
            ReversingPalindromeStrategy strategy = new ReversingPalindromeStrategy();
            string input = "to!p sp#ot";

            bool isInputPalindrome = strategy.CheckIfInputIsPalindrome(input);

            Assert.True(isInputPalindrome);
        }

        [Fact]
        public void ReturnFalseWhenInputIsNull()
        {
            ReversingPalindromeStrategy strategy = new ReversingPalindromeStrategy();
            string input = "";

            bool isInputPalindrome = strategy.CheckIfInputIsPalindrome(input);

            Assert.False(isInputPalindrome);
        }

        [Fact]
        public void ReturnFalseWhenInputIsNotPalindrome()
        {
            ReversingPalindromeStrategy strategy = new ReversingPalindromeStrategy();
            string input = "ab";

            bool isInputPalindrome = strategy.CheckIfInputIsPalindrome(input);

            Assert.False(isInputPalindrome);
        }
    }
}
