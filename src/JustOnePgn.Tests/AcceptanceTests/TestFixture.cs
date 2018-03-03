using System.IO;

namespace JustOnePgn.Tests.AcceptanceTests
{
    public class TestFixture
    {
        private static string Root = @"C:\Projects\Github\osotorrio\justonepgn\src\JustOnePgn.Tests\AcceptanceTests\Files";

        internal readonly string FolderWithOneFileOneGame = $@"{Root}\FolderWithOneFileOneGame";
        internal readonly string PathResultedPgn = $@"{Root}\ResultedFile\games.pgn";

        internal string ContentOfResultedPgn => File.ReadAllText(PathResultedPgn);

        internal string ContentOfExpectedOneGame => File.ReadAllText($@"{Root}\ExpectedFiles\OneFileOneGame.pgn");
    }
}