using JustOnePgn.Core.Contracts;

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

        public void Execute()
        {
            _reader.ReadGame(game => 
            {
                _writer.WriteGame(game);

                var isDuplicated = _repo.IsDuplicated(game);

                if (!isDuplicated)
                {
                    _repo.Save(game);
                }
            });
        }
    }
}