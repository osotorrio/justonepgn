using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Infrastructure;
using JustOnePgn.Core.Services;
using Shouldly;
using Xbehave;
using Xunit;
using NSubstitute;

namespace JustOnePgn.Tests.EndToEndTests
{
    [Collection("Create a single PGN file from multiple PGN files")]
    public class MultipleFilesScenarios : TestBase
    {
        [Scenario]
        public void TwoFilesTest(IReadPgnFiles reader, IWritePgnFiles writer, IGameRepository repo)
        {
            "GIVEN a folder with two files where each file contains one game".x(() =>
            {
                repo = Substitute.For<IGameRepository>();
                reader = new PgnReader(TestFixture.FolderWithTwoFiles);
                writer = new PgnWriter(TestFixture.PathResultedPgn);
            });

            "WHEN the files are read".x(() =>
            {
                var manager = new PgnManager(reader, writer, repo);
                manager.Execute(g => { });
            });

            "THEN the new file created contains two games".x(() =>
            {
                TestFixture.ContentOfResultedPgn.ShouldBe(TestFixture.ContentOfExpectedTwoGames);
            });
        }

        [Scenario]
        public void TwoNestedFilesTest(IReadPgnFiles reader, IWritePgnFiles writer, IGameRepository repo)
        {
            "GIVEN a folder with two nested files where each file contains one game".x(() =>
            {
                repo = Substitute.For<IGameRepository>();
                reader = new PgnReader(TestFixture.FolderWithNestedFiles);
                writer = new PgnWriter(TestFixture.PathResultedPgn);
            });

            "WHEN the files are read".x(() =>
            {
                var manager = new PgnManager(reader, writer, repo);
                manager.Execute(g => { });
            });

            "THEN the new file created contains two games".x(() =>
            {
                TestFixture.ContentOfResultedPgn.ShouldBe(TestFixture.ContentOfExpectedTwoGames);
            });
        }
    }
}
