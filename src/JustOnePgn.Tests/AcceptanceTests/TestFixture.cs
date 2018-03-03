using System.IO;

namespace JustOnePgn.Tests.AcceptanceTests
{
    public class TestFixture
    {
        private static string Root = @"C:\Projects\Github\osotorrio\justonepgn\src\JustOnePgn.Tests\AcceptanceTests\Files";

        // Games
        internal static readonly string FolderWithOneFileOneGame = $@"{Root}\FolderWithOneFileOneGame";
        internal static readonly string FolderWithOneFileTwoGames = $@"{Root}\FolderWithOneFileTwoGames";
        internal static readonly string FolderWithTwoFiles = $@"{Root}\FolderWithTwoFiles";
        internal static readonly string PathResultedPgn = $@"{Root}\ResultedFile\games.pgn";

        // Result
        internal static string ContentOfResultedPgn => File.ReadAllText(PathResultedPgn);

        // Expected
        internal static string ContentOfExpectedOneGame => File.ReadAllText($@"{Root}\ExpectedFiles\OneGame.pgn");
        internal static string ContentOfExpectedTwoGames => File.ReadAllText($@"{Root}\ExpectedFiles\TwoGames.pgn");
    }
}