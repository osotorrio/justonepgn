using JustOnePgn.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbehave;
using Shouldly;
using JustOnePgn.Core.Infrastructure;
using JustOnePgn.Core.Services;

namespace JustOnePgn.Tests.AcceptanceTests
{
    public class OneFileTests
    {
        [Scenario]
        public void OneGameTest(TestFixture fixture, IReadPgnFiles reader, IWritePgnFiles writer)
        {
            "GIVEN a single file with one game".x(() => 
            {
                fixture = new TestFixture();
                reader = new PgnReader(fixture.FolderWithOneFileOneGame);
                writer = new PgnWriter(fixture.PathResultedPgn);
            });

            "WHEN the file is read".x(() => 
            {
                var pgn = new PgnManager(reader, writer);
                pgn.Execute();
            });

            "THEN the file created contains one game".x(() => 
            {
                fixture.ContentOfResultedPgn.ShouldBe(fixture.ContentOfExpectedOneGame);
            });
        }
    }
}
