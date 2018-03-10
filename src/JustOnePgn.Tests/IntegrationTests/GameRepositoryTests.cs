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
            var result = repo.IsDuplicated(fixture.Game);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void IsDuplicatedGame_should_return_TRUE_when_duplicated_game()
        {
            // Arrange
            var fixture = new TestFixture();
            fixture.InsertGame(fixture.Game);
            var repo = new GameRepository(TestFixture.ConnectionString);

            // Act
            var result = repo.IsDuplicated(fixture.Game);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Save_should_store_the_game()
        {
            // Arrange
            var fixture = new TestFixture();
            var repo = new GameRepository(TestFixture.ConnectionString);

            // Act
            repo.Save(fixture.Game);

            // Assert
            var game = fixture.GetGameById(fixture.Game.GameId);

            game.ShouldNotBeNull();
            // game.GameId.ShouldBe(fixture.Game.GameId);
            game.Event.ShouldBe(fixture.Game.Event);
            game.White.ShouldBe(fixture.Game.White);
            game.Black.ShouldBe(fixture.Game.Black);
            game.Result.ShouldBe(fixture.Game.Result);
            game.WhiteElo.ShouldBe(fixture.Game.WhiteElo);
            game.BlackElo.ShouldBe(fixture.Game.BlackElo);
            game.Eco.ShouldBe(fixture.Game.Eco);
            //game.PlyCount.ShouldBe(fixture.Game.PlyCount);
            //game.Metadata.ShouldBe(fixture.Game.Metadata);
            //game.Moves.ShouldBe(fixture.Game.Moves);
        }
    }
}
