using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Domain;
using System;

namespace JustOnePgn.Core.Services
{
    public class PgnManager : IPgnManager
    {
        private readonly IReadPgnFiles _reader;
        private readonly IWritePgnFiles _writer;
        private readonly IGameRepository _repo;

        public PgnManager(IReadPgnFiles reader, IWritePgnFiles writer, IGameRepository repo)
        {
            _reader = reader;
            _writer = writer;
            _repo = repo;
        }

        public void Execute(Action<Game> callback)
        {
            _reader.ReadGame(game =>
            {

                var isDuplicated = _repo.IsDuplicated(game);

                if (!isDuplicated && game.PlyCount >= 30)
                {
                    // TODO: Do it transactional
                    _writer.WriteGame(game);
                    _repo.Save(game);
                }

                callback(game);
            });
        }
    }
}