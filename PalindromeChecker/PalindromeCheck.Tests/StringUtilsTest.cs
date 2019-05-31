using PalindromeCheck.Models;
using PalindromeCheck.Utilities;
using Xunit;

namespace PalindromeCheck.Tests
{
    public class StringUtilsTest
    {
        [Fact]
        public void RemoveAllPunctuationsFromInput()
        {
            string input = "a8nc *ff*^123";

            string result = StringUtils.RemovePunctuations(input);

            Assert.Equal("a8ncff123", result);
        }

        [Fact]
        public void ReturnTargetEnumWhenStrategyTypeIsValid()
        {
            string strategyTypeValue = "Recursive";

            PalindromeStrategyType strategyType = StringUtils.ToEnum(strategyTypeValue, PalindromeStrategyType.None);

            Assert.Equal(PalindromeStrategyType.Recursive, strategyType);
        }

        [Fact]
        public void ReturnDefualtEnumWhenStrategyTypeIsInvalid()
        {
            string strategyTypeValue = "Advanced";

            PalindromeStrategyType strategyType = StringUtils.ToEnum(strategyTypeValue, PalindromeStrategyType.None);

            Assert.Equal(PalindromeStrategyType.None, strategyType);
        }
    }
}
