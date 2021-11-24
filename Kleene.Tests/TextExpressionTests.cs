using Xunit;

namespace Kleene.Tests;

public class TextExpressionTests
{
    [Fact]
    public void Match()
    {
        // Given
        var expression = new TextExpression("foo");

        // When
        var result = expression.Transform("foo");

        // Then
        Assert.Equal("foo", result);
    }

    [Fact]
    public void Mismatch()
    {
        // Given
        var expression = new TextExpression("foo");

        // When
        var result = expression.Transform("bar");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void EmptyMatch()
    {
        // Given
        var expression = new TextExpression("");

        // When
        var result = expression.Transform("");

        // Then
        Assert.Equal("", result);
    }

    [Fact]
    public void EmptyMismatch()
    {
        // Given
        var expression = new TextExpression("");

        // When
        var result = expression.Transform("foo");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void EmptyInput()
    {
        // Given
        var expression = new TextExpression("foo");

        // When
        var result = expression.Transform("");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void PartialMatch()
    {
        // Given
        var expression = new TextExpression("foo");

        // When
        var result = expression.Transform("foobar");

        // Then
        Assert.Null(result);
    }
}
