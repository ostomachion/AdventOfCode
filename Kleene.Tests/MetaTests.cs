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
            var test = Type.GetType("Kleene.TextExpression+Model");
            var meta = Expression.Parse(Meta.Expression);
            Assert.NotNull(meta);

            var bootstrap = meta.RunFull(Meta.Expression2, out var captureTree);
            Assert.NotNull(captureTree);
        }
    }
}
