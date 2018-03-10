using System;
using JustOnePgn.Core.Domain;

namespace JustOnePgn.Core.Contracts
{
    public interface IGameRepository
    {
        void SaveGame(Game game);

        bool IsDuplicatedGame(Game game);
    }
}