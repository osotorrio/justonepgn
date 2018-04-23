using System;
using System.Collections.Generic;
using System.IO;
using JustOnePgn.Core.Contracts;

namespace JustOnePgn.Core.Infrastructure
{
    public class Logger : ILogger
    {
        private readonly string _logFile;

        private static string Separator => "================================================================================";

        public Logger(string logFile)
        {
            _logFile = logFile;
        }

        public void Info(string text)
        {
            var log = new List<string>
            {
                Separator,
                text,
                Separator,
            };

            File.AppendAllLines(_logFile, log);
        }

        public void Error(Exception exception, string text)
        {
            var log = new List<string>
            {
                exception.Message,
                Environment.NewLine,
                text
            };

            File.AppendAllLines(_logFile, log);
        }
    }
}