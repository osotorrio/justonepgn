using JustOnePgn.Core.Domain;
using Shouldly;
using System;
using Xunit;

namespace JustOnePgn.Tests.UnitTests
{
    public class GameTests
    {
        [Fact]
        public void PlyCount_should_count_all_moves_of_the_game()
        {
            // Arrange
            var line = "1.d4 Nf6 2.c4 e6 3.Nf3 d5 4.g3 dxc4 5.Bg2 c6";
            var pgn = new Pgn();
            pgn.Add(line);


            // Act
            var game = new Game(new Metadata(), pgn);

            // Assert
            game.PlyCount.ShouldBe(10);
        }

        [Fact]
        public void Metadata_should_have_a_new_line_between_tags()
        {
            // Arrange
            var line = "[key \"value\"]";
            var metadata = new Metadata();
            metadata.Add(line);
            metadata.Add(line);

            var newLineBetweenTags = $"{line}{Environment.NewLine}{line}{Environment.NewLine}[PlyCount \"0\"]";

            // Act
            var game = new Game(metadata, new Pgn());

            // Assert
            game.Metadata.ShouldBe(newLineBetweenTags);
        }

        [Fact]
        public void Metadata_should_setup_game_properties()
        {
            // Arrange
            var metadata = new Metadata();
            metadata.Add("[Event \"11th Kings Rapid women\"]");
            metadata.Add("[Date \"2017.11.27\"]");
            metadata.Add("[White \"Muzychuk, Anna\"]");
            metadata.Add("[Black \"Paehtz, Elisabeth\"]");
            metadata.Add("[Result \"1-0\"]");
            metadata.Add("[WhiteElo \"2576\"]");
            metadata.Add("[BlackElo \"2453\"]");
            metadata.Add("[ECO \"B29\"]");

            // Act
            var game = new Game(metadata, new Pgn());

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
            var line = "[Event \"11th Kings Rapid women\"]";
            var moves = "1.d4 Nf6 2.c4 e6 3.Nf3 d5 4.g3 dxc4 5.Bg2 c6 ";
            var emptyLineInBetween = $"{line}{Environment.NewLine}[PlyCount \"10\"]{Environment.NewLine}{Environment.NewLine}{moves}";

            var metadata = new Metadata();
            metadata.Add(line);

            var pgn = new Pgn();
            pgn.Add(moves);

            var game = new Game(metadata, pgn);

            // Act
            var text = game.ToString();

            // Assert
            text.ShouldBe(emptyLineInBetween);
        }
    }
}
