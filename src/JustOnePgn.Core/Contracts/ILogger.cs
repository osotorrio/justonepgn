using System;

namespace JustOnePgn.Core.Contracts
{
    public interface ILogger
    {
        void Info(string text);

        void Error(Exception exception, string text);
    }
}