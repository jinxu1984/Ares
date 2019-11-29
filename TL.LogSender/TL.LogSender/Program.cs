using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Fluent;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace TL.LogSender
{
    class Program
    {
        static Logger _logger = LogManager.GetCurrentClassLogger();
        
        static async Task Main(string[] args)
        {
            _logger.Info("============Start Program=============");
            
            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                
                var timeout = TimeSpan.FromSeconds(Int32.Parse(configuration["Timeout"]));
                var logFolder = new DirectoryInfo(configuration["LogDir"]);
                
                foreach (var logFile in logFolder.GetFiles())
                {
                    _logger.Info($"Process file =====> {logFile.FullName} ");
                    
                    Task processLogFile = await Task.Factory.StartNew(async () =>
                    {
                        List<string> logs = await new LogReader(logFile.FullName).ReadAsync();
                        var logSender = new LogSender(configuration["SendTo"], timeout);
                        await logSender.SendLogAsync(logs);
                    });

                    processLogFile.Wait();
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message);
                _logger.Error(exception.StackTrace);
            }

            _logger.Info("===========Complete Program==============");
        }
    }
}