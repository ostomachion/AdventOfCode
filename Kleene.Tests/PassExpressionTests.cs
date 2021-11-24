namespace Kleene.Tests;

public class PassExpressionTests
{
    [Fact]
    public void Match()
    {
        // Given
        var expression = new PassExpression();

        // When
        var result = expression.Transform("");

        // Then
        Assert.Equal("", result);
    }

    [Fact]
    public void NonEmpty()
    {
        // Given
        var expression = new PassExpression();

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Null(result);
    }
}
