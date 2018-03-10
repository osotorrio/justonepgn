using Dapper;
using JustOnePgn.Core.Domain;
using System.Data.SqlClient;
using System.Linq;

namespace JustOnePgn.Tests.IntegrationTests
{
    internal class TestFixture
    {
        internal static string ConnectionString => "Data Source=LENOVO-PC;Initial Catalog=ChessGamesDBTest;Integrated Security=True";

        internal Game Game { get; private set; }

        internal TestFixture()
        {
            Game = new Game();
            Game.AddMetadata("[Event \"Gibraltar Masters 2017\"]");
            Game.AddMetadata("[Site \"Caleta ENG\"]");
            Game.AddMetadata("[Date \"2017.01.29\"]");
            Game.AddMetadata("[Round \"6.65\"]");
            Game.AddMetadata("[White \"Heinemann, Josefine\"]");
            Game.AddMetadata("[Black \"Tsolakidou, Stavroula\"]");
            Game.AddMetadata("[Result \"1/2-1/2\"]");
            Game.AddMetadata("[WhiteElo \"2227\"]");
            Game.AddMetadata("[BlackElo \"2387\"]");
            Game.AddMetadata("[ECO \"B90\"]");
            Game.AddMetadata("[EventDate \"2017.01.24\"]");
            Game.AddMoves("1.e4 c5 2.Nf3 d6 3.d4 cxd4 4.Nxd4 Nf6 5.Nc3 a6 6.Be3 e5 7.Nb3 Be7 8.f3 Be6");
            Game.AddMoves("9.Qd2 O-O 10.O-O-O a5 11.a4 Nc6 12.g4 Nb4 13.Kb1 Qc7 14.Bb5 Rac8 15.g5 Nh5");
            Game.AddMoves("16.Rhg1 g6 17.Qf2 Bd8 18.Qd2 Be7 19.Qf2 Bd8 20.Qd2 Be7 21.Qf2 1/2-1/2");
        }

        internal Game GetGameById(int gameId)
        {
            using (var db = new SqlConnection(TestFixture.ConnectionString))
            {
                return db.Query<Game>("SELECT * FROM Games WHERE GameId = @GameId;", new { GameId = gameId }).FirstOrDefault();
            }
        }

        internal void InsertGame(Game game)
        {
            using (var db = new SqlConnection(TestFixture.ConnectionString))
            {
                var parameters = @"@GameId, @Event, @Year, 
                                   @White, @Black, @Result, 
                                   @WhiteElo, @BlackElo, @Eco, 
                                   @PlyCount, @Metadata, @Moves";

                db.Execute($"INSERT INTO Games VALUES ({parameters});", new
                {
                    game.GameId, game.Event, game.Year,
                    game.White, game.Black, game.Result,
                    game.WhiteElo, game.BlackElo, game.Eco,
                    game.PlyCount, game.Metadata, game.Moves
                });
            }
        }
    }
}