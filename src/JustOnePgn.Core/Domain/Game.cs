using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Services;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JustOnePgn.Core.Domain
{
    public class Game
    {
        public Game() { }

        public Game(IMetadata metadata, IPgn pgn)
        {
            Moves = pgn.Moves;

            // Adding PlyCount
            if (!metadata.Values.Any(tag => tag.Contains("[PlyCount ")))
            {
                metadata.Values.Add($"[PlyCount \"{PlyCount}\"]");
            }

            Metadata = string.Join(Environment.NewLine, metadata.Values);

            foreach (var tagPair in metadata.Values)
            {
                SetupTagPair(tagPair);
            }

            Hash = Sha256Service.GetHash(Moves);
        }

        public int GameId { get; private set; }

        public string Hash { get; private set; }

        public string Event { get; private set; }

        public int? Year { get; private set; }

        public string White { get; private set; }

        public string Black { get; private set; }

        public string Result { get; private set; }

        public int? WhiteElo { get; private set; }

        public int? BlackElo { get; private set; }

        public string Eco { get; private set; }

        public int PlyCount => Regex.Matches(Moves, PgnRegex.Moves).Count;

        public string Moves { get; set; }

        public string Metadata { get; set; }

        public string Source { get; set; }

        public override string ToString()
        {
            var pgn = new StringBuilder();
            pgn.Append(Metadata);
            pgn.Append(Environment.NewLine);
            pgn.Append(Environment.NewLine);
            pgn.Append(Moves);

            return pgn.ToString();
        }

        // Move away this logic from this class. Chain responsibility or Factory pattern?
        private void SetupTagPair(string tag)
        {
            var value = Regex.Match(tag, "\"" + "(.*?)" + "\"").Value.Trim('"');

            if (!string.IsNullOrWhiteSpace(value))
            {
                if (tag.StartsWith("[Event "))
                {
                    Event = value;
                }

                if (tag.StartsWith("[Date "))
                {
                    if (int.TryParse(value.Split('.').First(), out int result))
                    {
                        Year = result;
                    }
                }

                if (tag.StartsWith("[White "))
                {
                    White = value;
                }

                if (tag.StartsWith("[Black "))
                {
                    Black = value;
                }

                if (tag.StartsWith("[Result "))
                {
                    Result = value;
                }

                if (tag.StartsWith("[WhiteElo "))
                {
                    if (int.TryParse(value, out int result))
                    {
                        WhiteElo = result;
                    }
                }

                if (tag.StartsWith("[BlackElo "))
                {
                    if (int.TryParse(value, out int result))
                    {
                        BlackElo = result;
                    }
                }

                // TODO: Replace empty with own ECO detector (new nuget?).
                if (tag.StartsWith("[ECO "))
                {
                    Eco = value;
                }
            }
        }
    }
}