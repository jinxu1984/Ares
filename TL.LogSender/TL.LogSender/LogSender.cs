using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace TL.LogSender
{
    public class LogSender
    {
        private readonly string _hostUrl;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LogSender(string hostUrl) : this(hostUrl, TimeSpan.FromSeconds(5))
        {
        }

        public LogSender(string hostUrl, TimeSpan timeout)
        {
            _hostUrl = hostUrl;
        }

        public async Task SendLogAsync(List<string> messages)
        {
            double fastest = double.MaxValue;
            double average = 0;
            double slowest = long.MinValue;
            double totalSuccess = 0;
            double totalTime = 0;

            using (var httpClient = new HttpClient() {Timeout = TimeSpan.FromSeconds(5)})
            {
                for (int x = 0; x < messages.Count; x++)
                {
                    var content = new StringContent(messages[x], Encoding.UTF8, "application/text");
                
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    var response = await httpClient.PostAsync(_hostUrl, content);
                    watch.Stop();
                
                    var elapsedMilliseconds = watch.ElapsedMilliseconds;

                    if (elapsedMilliseconds > slowest)
                    {
                        slowest = elapsedMilliseconds;
                    }

                    if (elapsedMilliseconds < fastest)
                    {
                        fastest = elapsedMilliseconds;
                    }

                    totalTime += elapsedMilliseconds;

                    average = totalTime / (x + 1);
                    _logger.Trace($"elapsed: {elapsedMilliseconds}, fastest: {fastest}, slowest: {slowest}, average: {average}, total time: {totalTime}, total count: {x+1}") ;

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.Info($"Fail to send message: {x+1}, response code: {response.StatusCode}");
                    }
                    else
                    {
                        totalSuccess++;
                    }
                }
            }
            
            _logger.Info($"Fastest: {fastest}, slowest: {slowest}, average: {average}, total sucess: {totalSuccess}, totalMessage: {messages.Count}") ;
        }
    }
}