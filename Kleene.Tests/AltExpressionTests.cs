using System;
using Xunit;

namespace Kleene.Tests
{
    public class AltExpressionTests
    {
        [Fact]
        public void MatchFirst()
        {
            // Given
            var expression = new AltExpression(
                new TextExpression("x"),
                new TextExpression("y")
            );

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Equal("x", result);
        }

        [Fact]
        public void MatchSecond()
        {
            // Given
            var expression = new AltExpression(
                new TextExpression("x"),
                new TextExpression("y")
            );

            // When
            var result = expression.Transform("y");

            // Then
            Assert.Equal("y", result);
        }

        [Fact]
        public void MatchNeither()
        {
            // Given
            var expression = new AltExpression(
                new TextExpression("x"),
                new TextExpression("y")
            );

            // When
            var result = expression.Transform("z");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void EmptyInput()
        {
            // Given
            var expression = new AltExpression(
                new TextExpression("x"),
                new TextExpression("y")
            );

            // When
            var result = expression.Transform("");

            // Then
            Assert.Null(result);
        }
    }
}
