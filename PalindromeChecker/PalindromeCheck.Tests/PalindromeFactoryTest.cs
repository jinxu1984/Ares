using PalindromeCheck.Factory;
using PalindromeCheck.Interfaces;
using PalindromeCheck.Models;
using PalindromeCheck.Strategies;
using System.Collections.Generic;
using Xunit;

namespace PalindromeCheck.Tests
{
    public class PalindromeFactoryTest
    {
        PalindromeFactory factory;

        public PalindromeFactoryTest()
        {
            factory = new PalindromeFactory(new List<IPalindromeStrategy> { new RecursivePalindromeStrategy(), new ReversingPalindromeStrategy() });
        }

        [Fact]
        public void ReturnPalindromeStrategyIfStrategyTypeIsImplemented()
        {
            PalindromeStrategyType strategyType = PalindromeStrategyType.Recursive;
            IPalindromeStrategy strategy = factory.GetPalindromeStrategy(strategyType);

            Assert.IsType<RecursivePalindromeStrategy>(strategy);
        }

        [Fact]
        public void ThrowExceptionIfStrategyTypeIsNotImplemented()
        {
            PalindromeStrategyType strategyType = PalindromeStrategyType.NotImplementedStrategyType;
            Assert.Throws<PalindromeStrategyNotImplementedException>(() => factory.GetPalindromeStrategy(strategyType));
        }
    }
}
