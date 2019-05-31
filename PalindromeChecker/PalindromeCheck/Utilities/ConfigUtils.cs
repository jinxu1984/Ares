using Microsoft.Extensions.Configuration;
using System.IO;

namespace PalindromeCheck.Utilities
{
    // Summary:
    //     Exposes utility functions for application configuration
    public class ConfigUtils
    {
        public static string GetValueByKey(string key)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string value = configuration[key];

            return value;
        }
    }
}
