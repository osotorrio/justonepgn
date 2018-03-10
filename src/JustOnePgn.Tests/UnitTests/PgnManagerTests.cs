using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Domain;
using JustOnePgn.Core.Services;
using NSubstitute;
using System;
using Xunit;

namespace JustOnePgn.Tests.UnitTests
{
    public class PgnManagerTests
    {
        [Fact]
        public void Execute_should_call_ReadGame()
        {
            // Arrange
            var readerMock = Substitute.For<IReadPgnFiles>();
            var writerStub = Substitute.For<IWritePgnFiles>();
            var repoStub = Substitute.For<IGameRepository>();

            var manager = new PgnManager(readerMock, writerStub, repoStub);

            // Act
            manager.Execute();

            // Assert
            readerMock.Received().ReadGame(Arg.Any<Action<Game>>());
        }

        [Fact]
        public void Exectute_should_call_WriteGame_with_specific_Game()
        {
            // Arrange
            var expectedGame = new Game();
            var writerMock = Substitute.For<IWritePgnFiles>();
            var repoStub = Substitute.For<IGameRepository>();

            var readerStub = Substitute.For<IReadPgnFiles>();
            readerStub.ReadGame(Arg.Invoke(expectedGame));

            var manager = new PgnManager(readerStub, writerMock, repoStub);

            // Act
            manager.Execute();

            // Assert
            writerMock.Received().WriteGame(expectedGame);
        }

        [Fact]
        public void Exectute_should_call_SaveGame_with_when_it_is_not_a_duplicated_game()
        {
            // Arrange
            var expectedGame = new Game();

            var writerStub = Substitute.For<IWritePgnFiles>();
            var readerStub = Substitute.For<IReadPgnFiles>();
            var repoMock = Substitute.For<IGameRepository>();

            repoMock.IsDuplicatedGame(Arg.Any<Game>()).Returns(false);
            readerStub.ReadGame(Arg.Invoke(expectedGame));

            var manager = new PgnManager(readerStub, writerStub, repoMock);

            // Act
            manager.Execute();

            // Assert
            repoMock.Received().SaveGame(expectedGame);
        }

        [Fact]
        public void Exectute_should_not_call_SaveGame_when_it_is_a_duplicated_game()
        {
            // Arrange
            var expectedGame = new Game();

            var writerStub = Substitute.For<IWritePgnFiles>();
            var readerStub = Substitute.For<IReadPgnFiles>();
            var repoMock = Substitute.For<IGameRepository>();

            repoMock.IsDuplicatedGame(Arg.Any<Game>()).Returns(true);
            readerStub.ReadGame(Arg.Invoke(expectedGame));

            var manager = new PgnManager(readerStub, writerStub, repoMock);

            // Act
            manager.Execute();

            // Assert
            repoMock.DidNotReceive().SaveGame(expectedGame);
        }
    }
}
