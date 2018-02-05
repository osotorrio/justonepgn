using JustOnePgn.Core.Contracts;

namespace JustOnePgn.Core.Infrastructure
{
    public class PgnReader : IReadPgnFiles
    {
        private object folderWithOneFileOneGame;

        public PgnReader(object folderWithOneFileOneGame)
        {
            this.folderWithOneFileOneGame = folderWithOneFileOneGame;
        }
    }
}