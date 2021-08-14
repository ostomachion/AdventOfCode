using System;
using Xunit;

namespace Kleene.Tests
{
    public class FailExpressionTests
    {
        [Fact]
        public void Empty()
        {
            // Given
            var expression = new FailExpression();

            // When
            var result = expression.Transform("");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void NonEmpty()
        {
            // Given
            var expression = new FailExpression();

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Null(result);
        }
    }
}
