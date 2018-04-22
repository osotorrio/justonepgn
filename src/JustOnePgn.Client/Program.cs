using JustOnePgn.Core.Infrastructure;
using JustOnePgn.Core.Services;
using System;

namespace JustOnePgn.Client
{
    class Program
    {
        private static DateTime _start;
        private static DateTime _end;

        static void Main(string[] args)
        {
            _start = DateTime.UtcNow;

            var reader = new PgnReader(@"C:\Chess\PNGSources");
            var writer = new PgnWriter(@"C:\Chess\Databases\db2018_v2.pgn");
            var repo = new GameRepository(@"Data Source=LENOVO-PC;Initial Catalog=PlayGrandmasters;Integrated Security=True");
            var logger = new Logger(@"C:\Chess\Databases\logs_v2.txt");

            var manager = new PgnManager(reader, writer, repo, logger);

            int counter = 0;
            manager.Execute(game =>
            {
                counter++;

                string moves;

                if (game.Moves.Length >= 40)
                {
                    moves = game.Moves.Substring(game.Moves.Length - 40);
                }
                else
                {
                    moves = game.Moves;
                }

                Console.WriteLine($"{counter} = {game.Hash}: {moves}");
            });

            _end = DateTime.UtcNow;
            
            Console.WriteLine($"Duration: {_end - _start}");
            Console.Read();
        }
    }
}
