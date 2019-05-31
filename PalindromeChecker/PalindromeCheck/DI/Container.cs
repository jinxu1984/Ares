using Microsoft.Extensions.DependencyInjection;
using PalindromeCheck.Interfaces;
using PalindromeCheck.Models;
using PalindromeCheck.Factory;
using PalindromeCheck.Strategies;
using System;

namespace PalindromeCheck.DI
{
    // Summary:
    //     Exposes the funcitons to address dependency injections
    public class Container
    {
        public IServiceProvider ServiceProvider { get; private set; }


        // Summary:
        //     Initialze service provider and register services against it
        public Container()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<IPalindromeStrategy, RecursivePalindromeStrategy>();
            services.AddSingleton<IPalindromeStrategy, ReversingPalindromeStrategy>();
            services.AddSingleton<IPalindromeFactory, PalindromeFactory>();

            ServiceProvider = services.BuildServiceProvider();
        }

        // Summary:
        //     Get an instance from service provider
        //
        // Parameters:
        //   T:
        //     Generic type
        //
        // Returns:
        //     Retrun an instance of then given generic type
        public T GetInstance<T>()
        {
            T instance = default(T);

            try
            {
                instance = ServiceProvider.GetRequiredService<T>();
            }
            catch (Exception ex)
            {
                throw new ServiceNotRegisteredException(typeof(T).Name);
            }

            return instance;
        }
    }
}
