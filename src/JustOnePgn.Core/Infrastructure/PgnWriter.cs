using JustOnePgn.Core.Contracts;

namespace JustOnePgn.Core.Infrastructure
{
    public class PgnWriter : IWritePgnFiles
    {
        private object outputPgn;

        public PgnWriter(object outputPgn)
        {
            this.outputPgn = outputPgn;
        }
    }
}