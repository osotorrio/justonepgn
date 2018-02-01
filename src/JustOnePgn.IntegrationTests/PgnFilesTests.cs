using Xbehave;

namespace JustOnePgn.IntegrationTests
{
    public class PgnFilesTests
    {
        [Scenario]
        public void OneFileOneGameTest()
        {
            "GIVEN a folder with one PGN file".x(() => { });
            "AND the file contains one single game".x(() => { });
            "WHEN the file is processed".x(() => { });
            "THEN one single PGN file is created".x(() => { });
            "AND the file contains one game".x(() => { });
        }
    }
}
