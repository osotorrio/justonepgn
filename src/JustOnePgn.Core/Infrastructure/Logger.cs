using System;
using System.Collections.Generic;
using System.IO;
using JustOnePgn.Core.Contracts;

namespace JustOnePgn.Core.Infrastructure
{
    public class Logger : ILogger
    {
        private readonly string _folder;
        private string _file;

        private static string Separator => "================================================================================";

        public Logger(string folder)
        {
            _folder = folder;
            _file = Path.Combine(_folder, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.log");
            var fileStream = File.Create(_file);
            fileStream.Close();
        }

        public void Info(string text)
        {
            var log = new List<string>
            {
                Separator,
                text,
                Separator,
            };

            SetLogFile();
            File.AppendAllLines(_file, log);
        }

        public void Error(Exception exception, string text)
        {
            var log = new List<string>
            {
                exception.Message,
                Environment.NewLine,
                text
            };

            SetLogFile();
            File.AppendAllLines(_file, log);
        }

        private void SetLogFile()
        {
            if (new FileInfo(_file).Length > 500000) // 0.5 GB = 500000 bytes
            {
                _file = Path.Combine(_folder, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.log");
            }
        }
    }
}