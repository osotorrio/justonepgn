using JustOnePgn.Core.Contracts;
using Xbehave;
using Shouldly;
using JustOnePgn.Core.Infrastructure;
using JustOnePgn.Core.Services;
using Xunit;
using NSubstitute;

namespace JustOnePgn.Tests.EndToEndTests
{
    [Collection("Create a single PGN file from multiple PGN files")]
    public class OneFileScenarios : TestBase
    {
        [Scenario]
        public void OneGameTest(IReadPgnFiles reader, IWritePgnFiles writer, IGameRepository repo)
        {
            "GIVEN a single file with one game".x(() => 
            {
                repo = Substitute.For<IGameRepository>();
                reader = new PgnReader(TestFixture.FolderWithOneFileOneGame);
                writer = new PgnWriter(TestFixture.PathResultedPgn);
            });

            "WHEN the file is read".x(() => 
            {
                var manager = new PgnManager(reader, writer, repo);
                manager.Execute();
            });

            "THEN the new file created contains one game".x(() => 
            {
                TestFixture.ContentOfResultedPgn.ShouldBe(TestFixture.ContentOfExpectedOneGame);
            });
        }

        [Scenario]
        public void TwoGamesTest(IReadPgnFiles reader, IWritePgnFiles writer, IGameRepository repo)
        {
            "GIVEN a single file with two games".x(() =>
            {
                repo = Substitute.For<IGameRepository>();
                reader = new PgnReader(TestFixture.FolderWithOneFileTwoGames);
                writer = new PgnWriter(TestFixture.PathResultedPgn);
            });

            "WHEN the file is read".x(() =>
            {
                var manager = new PgnManager(reader, writer, repo);
                manager.Execute();
            });

            "THEN the new file created contains two games".x(() =>
            {
                TestFixture.ContentOfResultedPgn.ShouldBe(TestFixture.ContentOfExpectedTwoGames);
            });
        }
    }
}
