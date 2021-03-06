﻿using JustOnePgn.Core.Contracts;
using NSubstitute;
using System.IO;

namespace JustOnePgn.Tests.EndToEndTests
{
    public class TestFixture
    {
        internal static IGameRepository FakeRepo => Substitute.For<IGameRepository>();

        internal static ILogger FakeLogger => Substitute.For<ILogger>();

        private static string Root = @"C:\Projects\Github\osotorrio\justonepgn\src\JustOnePgn.Tests\EndToEndTests\Files";

        // Games
        internal static readonly string FolderWithOneFileOneGame = $@"{Root}\FolderWithOneFileOneGame";
        internal static readonly string FolderWithOneFileTwoGames = $@"{Root}\FolderWithOneFileTwoGames";
        internal static readonly string FolderWithTwoFiles = $@"{Root}\FolderWithTwoFiles";
        internal static readonly string FolderWithNestedFiles = $@"{Root}\FolderWithNestedFiles";
        internal static readonly string FolderWithOneFileWrongNotation = $@"{Root}\FolderWithOneFileWrongNotation";

        // Result
        internal static readonly string PathResultedPgn = $@"{Root}\ResultedFile\games.pgn";
        internal static string ContentOfResultedPgn => File.ReadAllText(PathResultedPgn);

        // Expected
        internal static string ContentOfExpectedOneGame => File.ReadAllText($@"{Root}\ExpectedFiles\OneGame.pgn");
        internal static string ContentOfExpectedTwoGames => File.ReadAllText($@"{Root}\ExpectedFiles\TwoGames.pgn");
        internal static string ContentOfWrongResultGames => File.ReadAllText($@"{Root}\ExpectedFiles\WrongResultGame.pgn");
    }
}