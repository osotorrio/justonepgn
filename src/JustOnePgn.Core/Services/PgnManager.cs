using JustOnePgn.Core.Contracts;

namespace JustOnePgn.Core.Services
{
    public class PgnManager : IPgnManager
    {
        private readonly IReadPgnFiles _reader;
        private readonly IWritePgnFiles _writer;

        public PgnManager(IReadPgnFiles reader, IWritePgnFiles writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public void Execute()
        {
            _reader.ReadGame(game => 
            {
                _writer.WriteGame(game);
            });
        }
    }
}