using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Domain;
using System.IO;

namespace JustOnePgn.Core.Infrastructure
{
    public class PgnWriter : IWritePgnFiles
    {
        private string _outputPgn;

        public PgnWriter(string outputPgn)
        {
            _outputPgn = outputPgn;
        }

        public void WriteGame(Game game)
        {
            File.AppendAllText(_outputPgn, game.ToString());
        }
    }
}