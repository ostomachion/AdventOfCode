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
            _ = meta.RunFull(Meta.Expression, out var captureTree);
            Assert.NotNull(captureTree);

            //for (var i = 0; i < 10; i++)
            //{
            //    _ = meta.RunFull(Meta.Expression2, out var captureTree);
            //    Assert.NotNull(captureTree);
            //    meta = captureTree!.Root.Parse<IModel<Expression>>()!.Convert();
            //}
            }
    }
}
