﻿using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Infrastructure;
using JustOnePgn.Core.Services;
using Shouldly;
using Xbehave;
using Xunit;

namespace JustOnePgn.Tests.AcceptanceTests
{
    [Collection("Create a single PGN file from multiple PGN files")]
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

            "WHEN the files are read".x(() =>
            {
                var manager = new PgnManager(reader, writer);
                manager.Execute();
            });

            "THEN the new file created contains two games".x(() =>
            {
                TestFixture.ContentOfResultedPgn.ShouldBe(TestFixture.ContentOfExpectedTwoGames);
            });
        }

        [Scenario]
        public void TwoNestedFilesTest(IReadPgnFiles reader, IWritePgnFiles writer)
        {
            "GIVEN a folder with two nested files where each file contains one game".x(() =>
            {
                reader = new PgnReader(TestFixture.FolderWithNestedFiles);
                writer = new PgnWriter(TestFixture.PathResultedPgn);
            });

            "WHEN the files are read".x(() =>
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
