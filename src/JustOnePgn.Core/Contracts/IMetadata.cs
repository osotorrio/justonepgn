using System.Collections.Generic;

namespace JustOnePgn.Core.Contracts
{
    public interface IMetadata
    {
        List<string> Values { get; }

        void Add(string line);
    }
}