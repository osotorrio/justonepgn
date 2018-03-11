using JustOnePgn.Core.Domain;
using Shouldly;
using System;
using System.Text;
using Xunit;

namespace JustOnePgn.Tests.UnitTests
{
    public class PgnTests
    {
        [Fact]
        public void Add_should_remove_extra_spaces()
        {
            // Arrange
            var pgn = new Pgn();
            var spaces = GenerateRandomSpaces();

            var line = $"1.d4 Nf6 2.c4 e6 3.Nf3 d5 4.g3 dxc4 5.Bg2{spaces}c6";
            var withoutSpaces = "1.d4 Nf6 2.c4 e6 3.Nf3 d5 4.g3 dxc4 5.Bg2 c6";

            // Act
            pgn.Add(line);

            // Assert
            pgn.Moves.ShouldBe(withoutSpaces);
        }

        [Fact]
        public void Add_should_remove_space_after_dot()
        {
            // Arrange
            var pgn = new Pgn();
            var spaces = GenerateRandomSpaces();

            var line = $"1. d4 Nf6 2. c4 e6 3. Nf3 d5 4. g3 dxc4 5. Bg2 c6";
            var withoutSpaces = "1.d4 Nf6 2.c4 e6 3.Nf3 d5 4.g3 dxc4 5.Bg2 c6";

            // Act
            pgn.Add(line);

            // Assert
            pgn.Moves.ShouldBe(withoutSpaces);
        }

        private string GenerateRandomSpaces()
        {
            var result = new StringBuilder();
            var size = new Random().Next(2, 100);

            for (int i = 0; i < size; i++)
            {
                result.Append(" ");
            }

            return result.ToString();
        }
    }
}
