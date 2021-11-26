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
            var meta = Expression.Meta.RunFull(Meta.Expression, out var captureTree);
            Assert.NotNull(meta);
            var metaMeta = captureTree!.Root.Parse<Expression>();
        }
    }
}
