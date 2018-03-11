using JustOnePgn.Core.Contracts;
using System.Collections.Generic;

namespace JustOnePgn.Core.Domain
{
    public class Metadata : IMetadata
    {
        private readonly List<string> _metadata = new List<string>();

        public void Add(string line)
        {
            _metadata.Add(line);
        }

        public List<string> Values => _metadata;
    }
}
