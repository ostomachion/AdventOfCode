using System.Linq;
using System;
using Xunit;

namespace Kleene.Tests
{
    public class BackreferenceExpressionTests
    {
        [Fact]
        public void Match()
        {
            // Given
            var expression = new ConcatExpression(new Expression[]
            {
                new CaptureExpression("foo", new CharExpression('x')),
                new BackreferenceExpression("foo")
            });

            // When
            var result = expression.Transform("xx");

            // Then
            Assert.Equal("xx", result);
        }

        [Fact]
        public void Mismatch()
        {
            // Given
            var expression = new ConcatExpression(new Expression[]
            {
                new CaptureExpression("foo", new CharExpression('x')),
                new BackreferenceExpression("foo")
            });

            // When
            var result = expression.Transform("xy");

            // Then
            Assert.Null(result);
        }
    }
}
