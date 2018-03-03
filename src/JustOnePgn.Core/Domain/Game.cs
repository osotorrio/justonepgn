using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JustOnePgn.Core.Domain
{
    public class Game
    {
        private readonly List<string> _metadata = new List<string>();
        private readonly StringBuilder _moves = new StringBuilder();

        public string Event { get; private set; }

        public ushort Date { get; private set; }

        public string White { get; private set; }

        public string Black { get; private set; }

        public string Result { get; private set; }

        public uint? WhiteElo { get; private set; }

        public uint? BlackElo { get; private set; }

        public string Eco { get; private set; }

        public int PlyCount { get { return Regex.Matches(Moves, PgnRegex.Moves).Count; } }

        public string Moves { get { return _moves.ToString(); }}

        public string Metadata { get { return string.Join(Environment.NewLine, _metadata); } }

        public void AddMoves(string line)
        {
            _moves.Append(line);
        }

        public void AddMetadata(string line)
        {
            _metadata.Add(line);
            SetupTagPair(line);
        }

        public override string ToString()
        {
            var pgn = new StringBuilder();
            pgn.Append(Metadata);
            pgn.Append(Environment.NewLine);
            pgn.Append(Environment.NewLine);
            pgn.Append(Moves);

            return pgn.ToString();
        }

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
                    if (ushort.TryParse(value.Split('.').First(), out ushort result))
                    {
                        Date = result;
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
                    if (uint.TryParse(value, out uint result))
                    {
                        WhiteElo = result;
                    }
                }

                if (tag.StartsWith("[BlackElo "))
                {
                    if (uint.TryParse(value, out uint result))
                    {
                        BlackElo = result;
                    }
                }

                if (tag.StartsWith("[ECO "))
                {
                    Eco = value;
                }
            }
        }
    }
}