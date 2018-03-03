using JustOnePgn.Core.Domain;
using System;

namespace JustOnePgn.Core.Contracts
{
    public interface IReadPgnFiles
    {
        void ReadGame(Action<Game> returnGame);
    }
}