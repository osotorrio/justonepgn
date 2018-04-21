using JustOnePgn.Core.Domain;
using JustOnePgn.Core.Infrastructure;
using Shouldly;
using Xunit;

namespace JustOnePgn.Tests.IntegrationTests
{
    public class GameRepositoryTests : TestBase
    {
        [Fact]
        public void IsDuplicatedGame_should_return_FALSE_when_new_game()
        {
            // Arrange
            var fixture = new TestFixture();
            var repo = new GameRepository(TestFixture.ConnectionString);

            // Act
            var result = repo.IsDuplicated(fixture.GetGame());

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void IsDuplicatedGame_should_return_TRUE_when_duplicated_game()
        {
            // Arrange
            var fixture = new TestFixture();
            fixture.InsertGame(fixture.GetGame());
            var repo = new GameRepository(TestFixture.ConnectionString);

            // Act
            var result = repo.IsDuplicated(fixture.GetGame());

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Save_should_store_the_game()
        {
            // Arrange
            var fixture = new TestFixture();
            var expectedGame = fixture.GetGame();
            var repo = new GameRepository(TestFixture.ConnectionString);

            // Act
            repo.Save(expectedGame);

            // Assert
            var actualGame = fixture.GetGameByHash(expectedGame.Hash);

            actualGame.ShouldNotBeNull();
            actualGame.GameId.ShouldNotBe(0);
            actualGame.Hash.ShouldBe(expectedGame.Hash);
            actualGame.Event.ShouldBe(expectedGame.Event);
            actualGame.White.ShouldBe(expectedGame.White);
            actualGame.Black.ShouldBe(expectedGame.Black);
            actualGame.Result.ShouldBe(expectedGame.Result);
            actualGame.WhiteElo.ShouldBe(expectedGame.WhiteElo);
            actualGame.BlackElo.ShouldBe(expectedGame.BlackElo);
            actualGame.Eco.ShouldBe(expectedGame.Eco);
            actualGame.PlyCount.ShouldBe(expectedGame.PlyCount);
            actualGame.Metadata.ShouldBe(expectedGame.Metadata);
            actualGame.Moves.ShouldBe(expectedGame.Moves);
        }

        [Fact]
        public void Save_should_store_an_empty_game()
        {
            // Arrange
            var fixture = new TestFixture();
            var expectedGame = fixture.GetEmptyGame();
            var repo = new GameRepository(TestFixture.ConnectionString);

            // Act
            repo.Save(expectedGame);

            // Assert
            var actualGame = fixture.GetGameByHash(expectedGame.Hash);

            actualGame.ShouldNotBeNull();
            actualGame.GameId.ShouldNotBe(0);
            actualGame.Hash.ShouldBe(expectedGame.Hash);
            actualGame.Event.ShouldBe(expectedGame.Event);
            actualGame.White.ShouldBe(expectedGame.White);
            actualGame.Black.ShouldBe(expectedGame.Black);
            actualGame.Result.ShouldBe(expectedGame.Result);
            actualGame.WhiteElo.ShouldBe(expectedGame.WhiteElo);
            actualGame.BlackElo.ShouldBe(expectedGame.BlackElo);
            actualGame.Eco.ShouldBe(expectedGame.Eco);
            actualGame.PlyCount.ShouldBe(expectedGame.PlyCount);
            actualGame.Metadata.ShouldBe(expectedGame.Metadata);
            actualGame.Moves.ShouldBe(expectedGame.Moves);
        }
    }
}
