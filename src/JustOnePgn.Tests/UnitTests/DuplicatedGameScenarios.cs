using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Domain;
using JustOnePgn.Core.Services;
using NSubstitute;
using Xbehave;

namespace JustOnePgn.Tests.UnitTests
{
    public class DuplicatedGameScenarios
    {
        [Scenario]
        public void AlwaysWriteGameTest(TestFixture fixture, Game game, IPgnManager manager)
        {
            "GIVEN a game".x(() => 
            {
                fixture = new TestFixture();
                game = new Game();
            });

            "WHEN the game gets read".x(() => 
            {
                fixture.FakeReader.ReadGame(Arg.Invoke(game));
            });

            "AND the game is processed".x(() => 
            {
                manager = new PgnManager(fixture.FakeReader, fixture.FakeWriter, fixture.FakeRepo);
                manager.Execute();
            });

            "THEN the same game is written".x(() => 
            {
                fixture.FakeWriter.Received().WriteGame(Arg.Is(game));
            });
        }

        [Scenario]
        public void NotDuplicatedGameTest(TestFixture fixture, Game game, IPgnManager manager)
        {
            "GIVEN a game".x(() => 
            {
                fixture = new TestFixture();

                game = new Game();
                fixture.FakeReader.ReadGame(Arg.Invoke(game));
            });

            "AND the game is not duplicated".x(() =>
            {
                fixture.FakeRepo.IsDuplicated(game).Returns(false);
            });

            "WHEN the game is processed".x(() =>
            {
                manager = new PgnManager(fixture.FakeReader, fixture.FakeWriter, fixture.FakeRepo);
                manager.Execute();
            });

            "THEN the game is stored in the database".x(() =>
            {
                fixture.FakeRepo.Received().Save(Arg.Is(game));
            });
        }

        [Scenario]
        public void DuplicatedGameTest(TestFixture fixture, Game game, IPgnManager manager)
        {
            "GIVEN a game".x(() =>
            {
                fixture = new TestFixture();

                game = new Game();
                fixture.FakeReader.ReadGame(Arg.Invoke(game));
            });

            "AND the game is duplicated".x(() =>
            {
                fixture.FakeRepo.IsDuplicated(game).Returns(true);
            });

            "WHEN the game is processed".x(() =>
            {
                manager = new PgnManager(fixture.FakeReader, fixture.FakeWriter, fixture.FakeRepo);
                manager.Execute();
            });

            "THEN the game is not stored in the database".x(() =>
            {
                fixture.FakeRepo.DidNotReceive().Save(Arg.Is(game));
            });
        }
    }
}
