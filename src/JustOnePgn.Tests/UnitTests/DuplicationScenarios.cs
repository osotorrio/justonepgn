using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Domain;
using JustOnePgn.Core.Services;
using NSubstitute;
using Xbehave;

namespace JustOnePgn.Tests.UnitTests
{
    public class DuplicationScenarios
    {
        [Scenario]
        public void ValidGameTest(TestFixture fixture, Game game, IPgnManager manager)
        {
            "GIVEN a game with at least 15 moves".x(() => 
            {
                fixture = new TestFixture();
                
                game = new Game(new Metadata(), fixture.PgnWithAtLeast15Moves);
                fixture.FakeReader.ReadGame(Arg.Invoke(game));
            });

            "AND the game is not duplicated".x(() =>
            {
                fixture.FakeRepo.IsDuplicated(game).Returns(false);
            });

            "WHEN the game is processed".x(() =>
            {
                manager = new PgnManager(fixture.FakeReader, fixture.FakeWriter, fixture.FakeRepo);
                manager.Execute(g => { });
            });

            "THEN the same game is written".x(() =>
            {
                fixture.FakeWriter.Received().WriteGame(Arg.Is(game));
            });

            "AND the game is stored in the database".x(() =>
            {
                fixture.FakeRepo.Received().Save(Arg.Is(game));
            });
        }

        [Scenario]
        public void GameDuplicatedTest(TestFixture fixture, Game game, IPgnManager manager)
        {
            "GIVEN a game".x(() =>
            {
                fixture = new TestFixture();

                game = new Game(new Metadata(), fixture.PgnWithAtLeast15Moves);
                fixture.FakeReader.ReadGame(Arg.Invoke(game));
            });

            "AND the game is duplicated".x(() =>
            {
                fixture.FakeRepo.IsDuplicated(game).Returns(true);
            });

            "WHEN the game is processed".x(() =>
            {
                manager = new PgnManager(fixture.FakeReader, fixture.FakeWriter, fixture.FakeRepo);
                manager.Execute(g => { });
            });

            "THEN the game is not written".x(() =>
            {
                fixture.FakeWriter.DidNotReceive().WriteGame(Arg.Is(game));
            });

            "AND the game is not stored in the database".x(() =>
            {
                fixture.FakeRepo.DidNotReceive().Save(Arg.Is(game));
            });
        }

        [Scenario]
        public void GameTooShortTest(TestFixture fixture, Game game, IPgnManager manager)
        {
            "GIVEN a game with less than 15 moves".x(() =>
            {
                fixture = new TestFixture();

                game = new Game(new Metadata(), fixture.PgnWithLessThan15Moves);
                fixture.FakeReader.ReadGame(Arg.Invoke(game));
            });

            "AND the game is not duplicated".x(() =>
            {
                fixture.FakeRepo.IsDuplicated(game).Returns(false);
            });

            "WHEN the game is processed".x(() =>
            {
                manager = new PgnManager(fixture.FakeReader, fixture.FakeWriter, fixture.FakeRepo);
                manager.Execute(g => { });
            });

            "THEN the game is not written".x(() =>
            {
                fixture.FakeWriter.DidNotReceive().WriteGame(Arg.Is(game));
            });

            "AND the game is not stored in the database".x(() =>
            {
                fixture.FakeRepo.DidNotReceive().Save(Arg.Is(game));
            });
        }
    }
}
