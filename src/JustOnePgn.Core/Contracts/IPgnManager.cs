using JustOnePgn.Core.Domain;
using System;

namespace JustOnePgn.Core.Contracts
{
    public interface IPgnManager
    {
        void ExecuteCheckingForDuplicates(Action<Game> callback);

        void ExecuteWithoutCheckingForDuplicates(Action<Game> callback);
    }
}