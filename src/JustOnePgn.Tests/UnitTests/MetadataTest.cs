using JustOnePgn.Core.Domain;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JustOnePgn.Tests.UnitTests
{
    public class MetadataTest
    {
        [Fact]
        public void Add_should_replace_result_with_star_by_draw()
        {
            // Arrange
            var metadata = new Metadata();
            var wrongResult = "[Result \"*\"]";
            var rightResult = "[Result \"1/2-1/2\"]";

            // Act
            metadata.Add(wrongResult);

            // Assert
            metadata.Values.First(result => result.Equals(rightResult));
        }
    }
}
