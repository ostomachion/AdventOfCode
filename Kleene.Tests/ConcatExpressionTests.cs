namespace Kleene.Tests;

public class ConcatExpressionTests
{
    [Fact]
    public void MatchFirst()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new TextExpression("x"),
            new TextExpression("y")
        });

        // When
        var result = expression.Transform("xy");

        // Then
        Assert.Equal("xy", result);
    }

    [Fact]
    public void Mismatch()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new TextExpression("x"),
            new TextExpression("y")
        });

        // When
        var result = expression.Transform("yx");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void PartialMatch()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new TextExpression("x"),
            new TextExpression("y")
        });

        // When
        var result = expression.Transform("xz");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void PartialMatchShort()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new TextExpression("x"),
            new TextExpression("y")
        });

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void PartialMatchLong()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new TextExpression("x"),
            new TextExpression("y")
        });

        // When
        var result = expression.Transform("xyz");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void Backtrack()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new OptExpression(new TextExpression("x")),
            new TextExpression("x")
        });

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Equal("x", result);
    }
}
