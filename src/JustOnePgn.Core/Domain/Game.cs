using JustOnePgn.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JustOnePgn.Core.Domain
{
    public class Game : IGame
    {
        private readonly List<string> _metadata;
        private readonly StringBuilder _moves;

        public Game()
        {
            _metadata = new List<string>();
            _moves = new StringBuilder();
        }

        public int GameId => Moves.GetHashCode();

        public string Event { get; private set; }

        public int Date { get; private set; }

        public string White { get; private set; }

        public string Black { get; private set; }

        public string Result { get; private set; }

        public int? WhiteElo { get; private set; }

        public int? BlackElo { get; private set; }

        public string Eco { get; private set; }

        public int PlyCount => Regex.Matches(Moves, PgnRegex.Moves).Count;

        // TODO: Replaces ". " by "." Which one should be first replaced...\s{2,} or ._
        public string Moves => Regex.Replace(_moves.ToString(), @"\s{2,}", " ");

        public string Metadata => string.Join(Environment.NewLine, _metadata);

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
                    if (int.TryParse(value.Split('.').First(), out int result))
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

                if (tag.StartsWith("[ECO "))
                {
                    Eco = value;
                }
            }
        }
    }
}