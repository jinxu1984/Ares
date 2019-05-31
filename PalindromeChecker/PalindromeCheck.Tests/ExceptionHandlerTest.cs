using PalindromeCheck.Models;
using PalindromeCheck.Utilities;
using System;
using Xunit;

namespace PalindromeCheck.Tests
{
    public class ExceptionHandlerTest
    {
        [Fact]
        public void CatchThenLogTheException()
        {
            string errorMessage = string.Empty;

            Action action = () => throw new PalindromeStrategyNotImplementedException("AdvancedPalidromeStrategy");
            Action<Exception> actionToLogException = (exception) => errorMessage = exception.Message;

            ExceptionHandler.TryCatch(action, actionToLogException);

            Assert.Equal("Palindrome Strategy - AdvancedPalidromeStrategy is not implemented", errorMessage);
        }
    }
}