using System;
using Xunit;

namespace Kleene.Tests
{
    public class CharExpressionTests
    {
        [Fact]
        public void Match()
        {
            // Given
            var expression = new CharExpression('x');

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Equal("x", result);
        }

        [Fact]
        public void Mismatch()
        {
            // Given
            var expression = new CharExpression('x');

            // When
            var result = expression.Transform("y");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void EmptyInput()
        {
            // Given
            var expression = new CharExpression('x');

            // When
            var result = expression.Transform("");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void PartialMatch()
        {
            // Given
            var expression = new CharExpression('x');

            // When
            var result = expression.Transform("xy");

            // Then
            Assert.Null(result);
        }
    }
}
