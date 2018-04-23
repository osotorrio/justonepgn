using JustOnePgn.Core.Domain;
using System;

namespace JustOnePgn.Core.Contracts
{
    public interface IPgnManager
    {
        void Execute(Action<Game> callback);

        void QuickExecute(Action<Game> callback);
    }
}