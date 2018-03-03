using JustOnePgn.Core.Contracts;
using Xbehave;
using Shouldly;
using JustOnePgn.Core.Infrastructure;
using JustOnePgn.Core.Services;
using Xunit;

namespace JustOnePgn.Tests.AcceptanceTests
{
    [Collection("Writting PGN file")]
    public class OneFileScenarios : BaseSecenario
    {
        [Scenario]
        public void OneGameTest(IReadPgnFiles reader, IWritePgnFiles writer)
        {
            "GIVEN a single file with one game".x(() => 
            {
                reader = new PgnReader(TestFixture.FolderWithOneFileOneGame);
                writer = new PgnWriter(TestFixture.PathResultedPgn);
            });

            "WHEN the file is read".x(() => 
            {
                var pgn = new PgnManager(reader, writer);
                pgn.Execute();
            });

            "THEN the file created contains one game".x(() => 
            {
                TestFixture.ContentOfResultedPgn.ShouldBe(TestFixture.ContentOfExpectedOneGame);
            });
        }

        [Scenario]
        public void TwoGamesTest(IReadPgnFiles reader, IWritePgnFiles writer)
        {
            "GIVEN a single file with two games".x(() =>
            {
                reader = new PgnReader(TestFixture.FolderWithOneFileTwoGames);
                writer = new PgnWriter(TestFixture.PathResultedPgn);
            });

            "WHEN the file is read".x(() =>
            {
                var pgn = new PgnManager(reader, writer);
                pgn.Execute();
            });

            "THEN the file created contains two games".x(() =>
            {
                TestFixture.ContentOfResultedPgn.ShouldBe(TestFixture.ContentOfExpectedTwoGames);
            });
        }
    }
}
