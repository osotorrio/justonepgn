using System;
using System.IO;

namespace JustOnePgn.Tests.EndToEndTests
{
    public abstract class TestBase : IDisposable
    {
        protected TestBase()
        {
            // Do "global" initialization here; Called before every test method.
        }

        public void Dispose()
        {
            File.WriteAllText(TestFixture.PathResultedPgn, string.Empty);
        }
    }
}
