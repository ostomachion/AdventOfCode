namespace Kleene.Tests;

public class OptExpressionTests
{
    [Fact]
    public void NonEmpty()
    {
        // Given
        var expression = new OptExpression(new TextExpression("x"));

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Equal("x", result);
    }

    [Fact]
    public void Empty()
    {
        // Given
        var expression = new OptExpression(new TextExpression("x"));

        // When
        var result = expression.Transform("");

        // Then
        Assert.Equal("", result);
    }

    // TODO: Lazy tests.
}
