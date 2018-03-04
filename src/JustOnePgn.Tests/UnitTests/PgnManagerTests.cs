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
            var manager = new PgnManager(readerMock, writerStub);

            // Act
            manager.Execute();

            // Assert
            readerMock.Received().ReadGame(Arg.Any<Action<Game>>());
        }

        [Fact]
        public void Exectute_should_call_WriteGame_with_a_Game()
        {
            // Arrange
            var expectedGame = new Game();
            var writerMock = Substitute.For<IWritePgnFiles>();
            var readerStub = Substitute.For<IReadPgnFiles>();
            readerStub.ReadGame(Arg.Invoke(expectedGame));

            var manager = new PgnManager(readerStub, writerMock);

            // Act
            manager.Execute();

            // Assert
            writerMock.Received().WriteGame(expectedGame);
        }
    }
}
