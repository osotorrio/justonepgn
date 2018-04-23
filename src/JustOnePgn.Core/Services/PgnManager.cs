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
        private readonly ILogger _logger;

        public PgnManager(IReadPgnFiles reader, IWritePgnFiles writer, IGameRepository repo, ILogger logger)
        {
            _reader = reader;
            _writer = writer;
            _repo = repo;
            _logger = logger;
        }

        public void Execute(Action<Game> callback)
        {
            _reader.ReadGame(game =>
            {
                var isDuplicated = _repo.IsDuplicated(game);

                if (!isDuplicated && game.PlyCount >= 30)
                {
                    try
                    {
                        // TODO: Do it transactional
                        _repo.Save(game);
                        _writer.WriteGame(game);
                    }
                    catch (Exception ex)
                    {
                        _logger.Info(game.Source);
                        _logger.Error(ex, game.ToString());
                    }
                }

                callback(game);
            });
        }

        public void QuickExecute(Action<Game> callback)
        {
            _reader.ReadGame(game =>
            {
                if (game.PlyCount >= 30)
                {
                    try
                    {
                        // TODO: Do it transactional
                        var wasSaved = _repo.QuickSave(game);

                        if (wasSaved)
                        {
                            _writer.WriteGame(game);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Info(game.Source);
                        _logger.Error(ex, game.ToString());
                    }
                }

                callback(game);
            });
        }
    }
}