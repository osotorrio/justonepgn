using JustOnePgn.Core.Domain;
using Shouldly;
using System;
using System.Text;
using Xunit;

namespace JustOnePgn.Tests.UnitTests
{
    public class GameTests
    {
        [Fact]
        public void AddMoves_should_remove_extra_spaces()
        {
            // Arrange
            var game = new Game();
            var spaces = GenerateRandomSpaces();

            var line = $"1.d4 Nf6 2.c4 e6 3.Nf3 d5 4.g3 dxc4 5.Bg2{spaces}c6";
            var withoutSpaces = "1.d4 Nf6 2.c4 e6 3.Nf3 d5 4.g3 dxc4 5.Bg2 c6";

            // Act
            game.AddMoves(line);

            // Assert
            game.Moves.ShouldBe(withoutSpaces);
        }

        [Fact]
        public void PlyCount_should_count_all_moves_of_the_game()
        {
            // Arrange
            var game = new Game();
            var line = "1.d4 Nf6 2.c4 e6 3.Nf3 d5 4.g3 dxc4 5.Bg2 c6";

            // Act
            game.AddMoves(line);

            // Assert
            game.PlyCount.ShouldBe(10);
        }

        [Fact]
        public void AddMetadata_should_add_a_new_line_between_tags()
        {
            // Arrange
            var game = new Game();
            var line = "[key \"value\"]";
            var newLineBetweenTags = $"{line}{Environment.NewLine}{line}";

            // Act
            game.AddMetadata(line);
            game.AddMetadata(line);

            // Assert
            game.Metadata.ShouldBe(newLineBetweenTags);
        }

        [Fact]
        public void AddMetadata_should_setup_game_properties()
        {
            // Arrange
            var game = new Game();

            // Act
            game.AddMetadata("[Event \"11th Kings Rapid women\"]");
            game.AddMetadata("[Date \"2017.11.27\"]");
            game.AddMetadata("[White \"Muzychuk, Anna\"]");
            game.AddMetadata("[Black \"Paehtz, Elisabeth\"]");
            game.AddMetadata("[Result \"1-0\"]");
            game.AddMetadata("[WhiteElo \"2576\"]");
            game.AddMetadata("[BlackElo \"2453\"]");
            game.AddMetadata("[ECO \"B29\"]");

            // Assert
            game.Event.ShouldBe("11th Kings Rapid women");
            game.Year.ShouldBe(2017);
            game.White.ShouldBe("Muzychuk, Anna");
            game.Black.ShouldBe("Paehtz, Elisabeth");
            game.Result.ShouldBe("1-0");
            game.WhiteElo.ShouldBe(2576);
            game.BlackElo.ShouldBe(2453);
            game.Eco.ShouldBe("B29");
        }

        [Fact]
        public void ToString_should_add_empty_line_between_metadata_and_moves()
        {
            // Arrange
            var game = new Game();
            var metadata = "[Event \"11th Kings Rapid women\"]";
            var moves = "1.d4 Nf6 2.c4 e6 3.Nf3 d5 4.g3 dxc4 5.Bg2 c6";
            var emptyLineInBetween = $"{metadata}{Environment.NewLine}{Environment.NewLine}{moves}";

            game.AddMetadata(metadata);
            game.AddMoves(moves);

            // Act
            var text = game.ToString();

            // Assert
            text.ShouldBe(emptyLineInBetween);
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
