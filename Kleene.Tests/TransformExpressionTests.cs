using System;
using Xunit;

namespace Kleene.Tests
{
    public class TransformExpressionTests
    {
        [Fact]
        public void Match()
        {
            // Given
            var expression = new TransformExpression(
                new CharExpression('x'),
                new CharExpression('y'));

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Equal("y", result);
        }

        [Fact]
        public void Mismatch()
        {
            // Given
            var expression = new TransformExpression(
                new CharExpression('x'),
                new CharExpression('y'));

            // When
            var result = expression.Transform("y");

            // Then
            Assert.Null(result);
        }
    }
}
