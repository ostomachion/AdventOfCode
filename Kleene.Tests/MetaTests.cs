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
            var meta = Expression.Meta;
            for (var i = 0; i < 10; i++)
            {
                _ = meta.RunFull(Meta.Expression, out var captureTree);
                Assert.NotNull(captureTree);
                meta = captureTree!.Root.Parse<Expression>();
                Assert.NotNull(meta);
            }
            // Neat :)
        }
    }
}
