using JustOnePgn.Core.Contracts;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustOnePgn.Tests.UnitTests
{
    public class TestFixture
    {
        public IReadPgnFiles FakeReader { get; private set; }

        public IWritePgnFiles FakeWriter { get; private set; }

        public IGameRepository FakeRepo { get; private set; }

        public TestFixture()
        {
            FakeReader = Substitute.For<IReadPgnFiles>();
            FakeWriter = Substitute.For<IWritePgnFiles>();
            FakeRepo = Substitute.For<IGameRepository>();
        }
    }
}
