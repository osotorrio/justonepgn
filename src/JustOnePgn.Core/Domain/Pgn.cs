using JustOnePgn.Core.Contracts;
using System.Text;
using System.Text.RegularExpressions;

namespace JustOnePgn.Core.Domain
{
    public class Pgn : IPgn
    {
        private readonly StringBuilder _moves = new StringBuilder();

        public void Add(string line)
        {
            _moves.Append(line);
        }

        // TODO: Replaces ". " by "." Which one should be first replaced...\s{2,} or ._
        public string Moves => Regex.Replace(_moves.ToString(), @"\s{2,}", " ");
    }
}
