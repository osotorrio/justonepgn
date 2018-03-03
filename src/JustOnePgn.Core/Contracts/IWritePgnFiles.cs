using JustOnePgn.Core.Domain;

namespace JustOnePgn.Core.Contracts
{
    public interface IWritePgnFiles
    {
        void WriteGame(Game game);
    }
}