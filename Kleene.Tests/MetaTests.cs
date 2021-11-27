using System;
using System.Linq;
using Xunit;

namespace Kleene.Tests
{
    public class MetaTests
    {
        [Fact]
        public void SelfRun()
        {
            var meta = Expression.Parse(Meta.Expression);
            Assert.NotNull(meta);

            var bootstrap = meta.RunFull(Meta.Expression2, out var captureTree);
            Assert.NotNull(captureTree);
        }
    }
}
