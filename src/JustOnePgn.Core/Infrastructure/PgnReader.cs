using System;
using System.IO;
using System.Text.RegularExpressions;
using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Domain;

namespace JustOnePgn.Core.Infrastructure
{
    public class PgnReader : IReadPgnFiles
    {
        private readonly string _folder;

        public PgnReader(string folder)
        {
            _folder = folder;
        }

        public void ReadGame(Action<Game> returnGame)
        {
            var metadata = new Metadata();
            var pgn = new Pgn();
            var files = Directory.EnumerateFiles(_folder, "*.pgn", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (ContainsMetadata(line))
                        {
                            metadata.Add(line);
                        }

                        if (ContainsMoves(line))
                        {
                            pgn.Add(line);
                        }

                        if (ContainsResultAtTheEnd(line))
                        {
                            returnGame(new Game(metadata, pgn));
                            metadata = new Metadata();
                            pgn = new Pgn();
                        }
                    }
                }
            }
        }

        private static bool ContainsMetadata(string line)
        {
            return Regex.IsMatch(line, PgnRegex.Metadata, RegexOptions.Singleline);
        }

        private static bool ContainsMoves(string line)
        {
            return Regex.IsMatch(line, PgnRegex.Moves, RegexOptions.Singleline);
        }

        private static bool ContainsResultAtTheEnd(string line)
        {
            return Regex.IsMatch(line, PgnRegex.ResultAtTheEnd, RegexOptions.Singleline);
        }
    }
}