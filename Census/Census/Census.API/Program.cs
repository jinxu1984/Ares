using Census.API.Infrastructure;
using Census.API.Infrastructure.WebHost;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Census.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
            .MigrateDbContext<CensusDbContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<CensusDbContextSeed>>();

                new CensusDbContextSeed()
                    .SeedAsync(context, logger)
                    .Wait();
            })
            .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
