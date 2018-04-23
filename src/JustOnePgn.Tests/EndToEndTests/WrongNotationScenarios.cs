using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Infrastructure;
using JustOnePgn.Core.Services;
using Shouldly;
using Xbehave;
using Xunit;

namespace JustOnePgn.Tests.EndToEndTests
{
    [Collection("Create a single PGN file from multiple PGN files")]
    public class WrongNotationScenarios : TestBase
    {
        [Scenario]
        public void WrongResultNotationTest(IReadPgnFiles reader, IWritePgnFiles writer)
        {
            "GIVEN a single file with wrong result notation".x(() =>
            {
                reader = new PgnReader(TestFixture.FolderWithOneFileWrongNotation);
                writer = new PgnWriter(TestFixture.PathResultedPgn);
            });

            "WHEN the file is read".x(() =>
            {
                var manager = new PgnManager(reader, writer, TestFixture.FakeRepo, TestFixture.FakeLogger);
                manager.ExecuteCheckingForDuplicates(g => { });
            });

            "THEN the new file created contains two games".x(() =>
            {
                TestFixture.ContentOfResultedPgn.ShouldBe(TestFixture.ContentOfWrongResultGames);
            });
        }
    }
}
