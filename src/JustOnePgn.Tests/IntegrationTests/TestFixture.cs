using Dapper;
using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Domain;
using System.Data.SqlClient;
using System.Linq;

namespace JustOnePgn.Tests.IntegrationTests
{
    internal class TestFixture
    {
        internal static string ConnectionString => "Data Source=LENOVO-PC;Initial Catalog=PlayGrandmastersIntegrationTests;Integrated Security=True";

        internal Game GetEmptyGame()
        {
            var pgn = new Pgn();
            pgn.Add("1.e4 c5 2.Nf3 d6 3.d4 cxd4 4.Nxd4 Nf6 5.Nc3 a6 6.Be3 e5 7.Nb3 Be7 8.f3 Be6");
            pgn.Add("9.Qd2 O-O 10.O-O-O a5 11.a4 Nc6 12.g4 Nb4 13.Kb1 Qc7 14.Bb5 Rac8 15.g5 Nh5");
            pgn.Add("16.Rhg1 g6 17.Qf2 Bd8 18.Qd2 Be7 19.Qf2 Bd8 20.Qd2 Be7 21.Qf2 1/2-1/2");

            return new Game(new Metadata(), pgn);
        }

        internal Game GetGame()
        {
            var metadata = new Metadata();
            metadata.Add("[Event \"Gibraltar Masters 2017\"]");
            metadata.Add("[Site \"Caleta ENG\"]");
            metadata.Add("[Date \"2017.01.29\"]");
            metadata.Add("[Round \"6.65\"]");
            metadata.Add("[White \"Heinemann, Josefine\"]");
            metadata.Add("[Black \"Tsolakidou, Stavroula\"]");
            metadata.Add("[Result \"1/2-1/2\"]");
            metadata.Add("[WhiteElo \"2227\"]");
            metadata.Add("[BlackElo \"2387\"]");
            metadata.Add("[ECO \"B90\"]");
            metadata.Add("[EventDate \"2017.01.24\"]");

            var pgn = new Pgn();
            pgn.Add("1.e4 c5 2.Nf3 d6 3.d4 cxd4 4.Nxd4 Nf6 5.Nc3 a6 6.Be3 e5 7.Nb3 Be7 8.f3 Be6");
            pgn.Add("9.Qd2 O-O 10.O-O-O a5 11.a4 Nc6 12.g4 Nb4 13.Kb1 Qc7 14.Bb5 Rac8 15.g5 Nh5");
            pgn.Add("16.Rhg1 g6 17.Qf2 Bd8 18.Qd2 Be7 19.Qf2 Bd8 20.Qd2 Be7 21.Qf2 1/2-1/2");

            return new Game(metadata, pgn);
        }

        internal Game GetGameByHash(string hash)
        {
            using (var db = new SqlConnection(TestFixture.ConnectionString))
            {
                return db.Query<Game>("SELECT * FROM Games WHERE Hash = @Hash;", new { Hash = hash }).FirstOrDefault();
            }
        }

        internal void InsertGame(Game game)
        {
            using (var db = new SqlConnection(TestFixture.ConnectionString))
            {
                var parameters = @"@Hash, @Event, @Year, 
                                   @White, @Black, @Result, 
                                   @WhiteElo, @BlackElo, @Eco, 
                                   @PlyCount, @Metadata, @Moves";

                db.Execute($@"INSERT INTO Games 
                            (Hash, Event, Year, White, Black, Result, WhiteElo, BlackElo, Eco, PlyCount, Metadata, Moves) 
                            VALUES ({parameters});", new
                {
                    game.Hash, game.Event, game.Year,
                    game.White, game.Black, game.Result,
                    game.WhiteElo, game.BlackElo, game.Eco,
                    game.PlyCount, game.Metadata, game.Moves
                });
            }
        }
    }
}