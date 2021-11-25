namespace Kleene.Tests;

public class SubExpressionTests
{
    [Fact]
    public void Backreference()
    {
        // Given
        var expression = new ConcatExpression(
            new AssignmentExpression("foo", "x"),
            new SubExpression(new BackreferenceExpression("foo"), new TextExpression("x"))
        );

        // When
        var result = expression.Transform("");

        // Then
        Assert.Equal("", result);
    }

    [Fact]
    public void BackreferenceMismatch()
    {
        // Given
        var expression = new ConcatExpression(
            new AssignmentExpression("foo", "x"),
            new SubExpression(new BackreferenceExpression("foo"), new TextExpression("y"))
        );

        // When
        var result = expression.Transform("");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void BackreferenceUndefined()
    {
        // Given
        var expression = new SubExpression(new BackreferenceExpression("foo"), new TextExpression("x"));

        // When
        var result = expression.Transform("");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void BackreferenceExistence()
    {
        // Given
        var expression = new ConcatExpression(new Expression[] {
            new AssignmentExpression("foo", "x"),
            new SubExpression(new BackreferenceExpression("foo"))
        });

        // When
        var result = expression.Transform("");

        // Then
        Assert.Equal("", result);
    }

    [Fact]
    public void BackreferenceExistenceUndefined()
    {
        // Given
        var expression = new ConcatExpression(new Expression[] {
            new SubExpression(new BackreferenceExpression("foo"))
        });

        // When
        var result = expression.Transform("");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void Text()
    {
        // Given
        var expression = new SubExpression("x", new TextExpression("x"));

        // When
        var result = expression.Transform("");

        // Then
        Assert.Equal("", result);
    }

    [Fact]
    public void TextMismatch()
    {
        // Given
        var expression = new SubExpression("x", new TextExpression("y"));

        // When
        var result = expression.Transform("");

        // Then
        Assert.Null(result);
    }
}
