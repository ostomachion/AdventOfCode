namespace Kleene.Tests;

public class BackreferenceExpressionTests
{
    [Fact]
    public void Match()
    {
        // Given
        var expression = new ConcatExpression(
            new CaptureExpression("foo", new TextExpression("x")),
            new BackreferenceExpression("foo")
        );

        // When
        var result = expression.Transform("xx");

        // Then
        Assert.Equal("xx", result);
    }

    [Fact]
    public void Mismatch()
    {
        // Given
        var expression = new ConcatExpression(
            new CaptureExpression("foo", new TextExpression("x")),
            new BackreferenceExpression("foo")
        );

        // When
        var result = expression.Transform("xy");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void Undefined()
    {
        // Given
        var expression = new ConcatExpression(
            new CaptureExpression("foo", new TextExpression("x")),
            new BackreferenceExpression("bar")
        );

        // When
        var result = expression.Transform("xx");

        // Then
        Assert.Null(result);
    }

    [Fact]
    public void Shadow()
    {
        // Given
        var expression = new ConcatExpression(
            new CaptureExpression("foo", new TextExpression("x")),
            new CaptureExpression("foo", new TextExpression("y")),
            new BackreferenceExpression("foo")
        );

        // When
        var result = expression.Transform("xyy");

        // Then
        Assert.Equal("xyy", result);
    }

    [Fact]
    public void Nested()
    {
        // Given
        var expression = new ConcatExpression(
            new CaptureExpression("foo", new CaptureExpression("bar", new TextExpression("x"))),
            new BackreferenceExpression("foo.bar")
        );

        // When
        var result = expression.Transform("xx");

        // Then
        Assert.Equal("xx", result);
    }


    [Fact]
    public void Ancestor()
    {
        // Given
        var expression = new ConcatExpression(
            new AssignmentExpression("foo", "x"),
            new CaptureExpression("bar", new BackreferenceExpression("foo"))
        );

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Equal("x", result);
    }


    [Fact]
    public void AncestorNested()
    {
        // Given
        var expression = new ConcatExpression(
            new CaptureExpression("foo", new AssignmentExpression("bar", "x")),
            new CaptureExpression("baz", new BackreferenceExpression("foo.bar"))
        );

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Equal("x", result);
    }


    [Fact]
    public void BlockDescendants()
    {
        // Given
        var expression = new ConcatExpression(
            new CaptureExpression("foo", new AssignmentExpression("bar", "x")),
            new BackreferenceExpression("bar")
        );

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Null(result);
    }


    [Fact]
    public void OpenCapture()
    {
        // Given
        var expression = new TransformExpression(
            new PassExpression(),
            new CaptureExpression("foo", new BackreferenceExpression("foo"))
        );

        // When
        var result = expression.Transform("");

        // Then
        Assert.Null(result);
    }
}
