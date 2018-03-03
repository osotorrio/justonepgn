using System;
using System.IO;

namespace JustOnePgn.Tests.AcceptanceTests
{
    public abstract class BaseSecenario : IDisposable
    {
        protected BaseSecenario()
        {
            // Do "global" initialization here; Called before every test method.
        }

        public void Dispose()
        {
            File.WriteAllText(TestFixture.PathResultedPgn, string.Empty);
        }
    }
}
