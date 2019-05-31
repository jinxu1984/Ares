using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PalindromeCheck.DI;
using PalindromeCheck.Interfaces;
using PalindromeCheck.Factory;
using PalindromeCheck.Models;

namespace PalindromeCheck.Tests
{
    public class ContainerTest
    {
        private Container container;

        public ContainerTest()
        {
            container = new Container();
        }

        [Fact]
        public void GetInstanceByTypeIfTypeIsConfiguredInContainer()
        {
            IPalindromeFactory factory = container.GetInstance<IPalindromeFactory>();

            Assert.IsType<PalindromeFactory>(factory);
        }


        [Fact]
        public void ThrowExcpetionWhenTypeIsNotConfiguredInContainer()
        {
            Assert.Throws<ServiceNotRegisteredException>(() => container.GetInstance<PalindromeStrategyType>());
        }
    }
}
