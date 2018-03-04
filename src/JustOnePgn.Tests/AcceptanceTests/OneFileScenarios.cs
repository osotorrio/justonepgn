using JustOnePgn.Core.Contracts;
using Xbehave;
using Shouldly;
using JustOnePgn.Core.Infrastructure;
using JustOnePgn.Core.Services;
using Xunit;

namespace JustOnePgn.Tests.AcceptanceTests
{
    [Collection("Create a single PGN file from multiple PGN files")]
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
                var manager = new PgnManager(reader, writer);
                manager.Execute();
            });

            "THEN the new file created contains one game".x(() => 
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
                var manager = new PgnManager(reader, writer);
                manager.Execute();
            });

            "THEN the new file created contains two games".x(() =>
            {
                TestFixture.ContentOfResultedPgn.ShouldBe(TestFixture.ContentOfExpectedTwoGames);
            });
        }
    }
}
