namespace Kleene.Tests;

public class AssignmentExpressionTests
{
    [Fact]
    public void Text()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new AssignmentExpression("foo", "x"),
            new BackreferenceExpression("foo")
        });

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Equal("x", result);
    }

    [Fact]
    public void Capture()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new CaptureExpression("foo", new TextExpression("x")),
            new AssignmentExpression("bar", new BackreferenceExpression("foo")),
            new BackreferenceExpression("bar")
        });

        // When
        var result = expression.Transform("xx");

        // Then
        Assert.Equal("xx", result);
    }

    [Fact]
    public void Shadow()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new CaptureExpression("foo", new TextExpression("x")),
            new AssignmentExpression("foo", "y"),
            new BackreferenceExpression("foo")
        });

        // When
        var result = expression.Transform("xy");

        // Then
        Assert.Equal("xy", result);
    }

    [Fact]
    public void Nested()
    {
        // Given
        var expression = new ConcatExpression(new Expression[]
        {
            new AssignmentExpression("foo.bar", "x"),
            new BackreferenceExpression("foo.bar")
        });

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Equal("x", result);
    }
}
