using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Infrastructure;
using JustOnePgn.Core.Services;
using Shouldly;
using Xbehave;
using Xunit;

// https://xunit.github.io/docs/running-tests-in-parallel.html

namespace JustOnePgn.Tests.AcceptanceTests
{
    [Collection("Writting PGN file")]
    public class MultipleFilesScenarios : BaseSecenario
    {
        [Scenario]
        public void TwoFilesTest(IReadPgnFiles reader, IWritePgnFiles writer)
        {
            "GIVEN a folder with two files where each file contains one game".x(() =>
            {
                reader = new PgnReader(TestFixture.FolderWithTwoFiles);
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
