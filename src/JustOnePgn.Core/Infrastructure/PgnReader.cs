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
            var game = new Game();
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
                            game.AddMetadata(line);
                        }

                        if (ContainsMoves(line))
                        {
                            game.AddMoves(line);
                        }

                        if (ContainsResultAtTheEnd(line))
                        {
                            returnGame(game);
                            game = new Game();
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

        private bool ContainsResultAtTheEnd(string line)
        {
            return Regex.IsMatch(line, PgnRegex.ResultAtTheEnd, RegexOptions.Singleline);
        }
    }
}