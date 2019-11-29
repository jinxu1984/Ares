using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TL.LogSender
{
    public class LogReader
    {
        private readonly string _logFilePath;
        
        public LogReader(string logFilePath)
        {
            _logFilePath = logFilePath;
        }
        
        public async Task<List<string>> ReadAsync()
        {
            List<string> logs = new List<string>();

            await using var fileStream = new FileStream(_logFilePath, 
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var streamReader = new StreamReader(fileStream);
            while (streamReader.Peek() >= 0)
            {
                string log = await streamReader.ReadLineAsync();
                if (log is {}) {logs.Add(log);}
            }
            
            streamReader.Close();

            return logs;
        }
    }
}