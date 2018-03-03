namespace JustOnePgn.Core.Contracts
{
    public interface IGame
    {
        string Black { get; }

        uint? BlackElo { get; }

        ushort Date { get; }

        string Eco { get; }

        string Event { get; }

        string Metadata { get; }

        string Moves { get; }

        int PlyCount { get; }

        string Result { get; }

        string White { get; }

        uint? WhiteElo { get; }

        void AddMetadata(string line);

        void AddMoves(string line);

        string ToString();
    }
}