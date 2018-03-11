using JustOnePgn.Core.Contracts;
using System.Collections.Generic;

namespace JustOnePgn.Core.Domain
{
    public class Metadata : IMetadata
    {
        private readonly List<string> _metadata = new List<string>();

        public void Add(string line)
        {
            line = line.Replace("[Result \"*\"]", "[Result \"1/2-1/2\"]");
            _metadata.Add(line);
        }

        public List<string> Values => _metadata;
    }
}
