using System.Linq;
using System;
using Xunit;

namespace Kleene.Tests
{
    public class CallExpressionTests
    {
        [Fact]
        public void Match()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new TextExpression("x")),
                new CallExpression("foo")
            });

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Equal("x", result);
        }

        [Fact]
        public void Mismatch()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new TextExpression("x")),
                new CallExpression("foo")
            });

            // When
            var result = expression.Transform("y");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void Undefined()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new TextExpression("x")),
                new CallExpression("bar")
            });

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void Shadow()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new TextExpression("x")),
                new FunctionExpression("foo", new TextExpression("y")),
                new CallExpression("foo")
            });

            // When
            var result = expression.Transform("y");

            // Then
            Assert.Equal("y", result);
        }
    }
}
