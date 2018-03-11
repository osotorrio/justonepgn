using JustOnePgn.Core.Contracts;
using JustOnePgn.Core.Domain;
using NSubstitute;

namespace JustOnePgn.Tests.UnitTests
{
    public class TestFixture
    {
        public IReadPgnFiles FakeReader { get; private set; }

        public IWritePgnFiles FakeWriter { get; private set; }

        public IGameRepository FakeRepo { get; private set; }

        public IPgn PgnWithAtLeast15Moves { get; private set; }

        public IPgn PgnWithLessThan15Moves { get; private set; }

        public TestFixture()
        {
            FakeReader = Substitute.For<IReadPgnFiles>();
            FakeWriter = Substitute.For<IWritePgnFiles>();
            FakeRepo = Substitute.For<IGameRepository>();

            PgnWithAtLeast15Moves = new Pgn();
            PgnWithAtLeast15Moves.Add("1.e4 e5 2.Bc4 Bc5 3.c3 Nf6 4.d4 exd4 5.cxd4 Bb6 6.Nc3 O-O 7.Nge2 c6 8.Bd3 d5 9.e5 Ne8 10.Be3 f6 11.Qd2 fxe5 12.dxe5 Be6 13.Nf4 Qe7 14.Bxb6 axb6 15.O-O Nd7");

            PgnWithLessThan15Moves = new Pgn();
            PgnWithLessThan15Moves.Add("1.e4 e5 2.Bc4 Bc5 3.c3 Nf6 4.d4 exd4 5.cxd4 Bb6 6.Nc3 O-O 7.Nge2 c6 8.Bd3 d5");
        }
    }
}
