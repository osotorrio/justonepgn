namespace JustOnePgn.Core.Contracts
{
    public interface IPgn
    {
        string Moves { get; }

        void Add(string line);
    }
}