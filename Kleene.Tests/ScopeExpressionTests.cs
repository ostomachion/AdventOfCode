using System.Linq;
using System;
using Xunit;

namespace Kleene.Tests
{
    public class ScopeExpressionTests
    {
        [Fact]
        public void BlockDescendants()
        {
            // Given
            var expression = new ConcatExpression(new Expression[]
            {
                new CaptureExpression("foo", new AssignmentExpression("bar", "x")),
                new ScopeExpression("foo", new BackreferenceExpression("bar"))
            });

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Equal("x", result);
        }
    }
}
