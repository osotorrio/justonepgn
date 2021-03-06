﻿using JustOnePgn.Core.Domain;

namespace JustOnePgn.Core.Contracts
{
    public interface IGameRepository
    {
        void Save(Game game);

        bool IsDuplicated(Game game);
    }
}