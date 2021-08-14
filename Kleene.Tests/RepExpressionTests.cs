using System;
using Xunit;

namespace Kleene.Tests
{
    public class RepExpressionTests
    {
        [Fact]
        public void Match()
        {
            // Given
            var expression = new RepExpression(new CharExpression('x'), new RepCount(0, -1));

            // When
            var result = expression.Transform("xxxx");

            // Then
            Assert.Equal("xxxx", result);
        }

        // TODO: More tests.
    }
}
