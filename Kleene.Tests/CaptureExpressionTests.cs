using System.Linq;
using System;
using Xunit;

namespace Kleene.Tests
{
    public class CaptureExpressionTests
    {
        [Fact]
        public void Match()
        {
            // Given
            var expression = new CaptureExpression("foo", new CharExpression('x'));

            // When
            var context = new ExpressionContext("x");
            var result = expression.Run(context).FirstOrDefault();

            // Then
            Assert.Equal("x", result?.Input);
            Assert.Equal("x", result?.Output);
            Assert.Collection(context.CaptureTree.Current.Children,
                item =>
                {
                    Assert.Equal("foo", item.Name);
                    Assert.Collection(item.Values,
                    item =>
                    {
                        Assert.NotNull(item);
                        Assert.Equal("x", item.Input);
                        Assert.Equal("x", item.Output);
                    });
                }
            );
        }
    }
}
