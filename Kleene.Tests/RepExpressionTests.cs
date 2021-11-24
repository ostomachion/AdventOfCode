namespace Kleene.Tests;

public class RepExpressionTests
{
    [Fact]
    public void Match()
    {
        // Given
        var expression = new RepExpression(new TextExpression("x"), null, new RepCount(0, -1));

        // When
        var result = expression.Transform("xxxx");

        // Then
        Assert.Equal("xxxx", result);
    }

    [Fact]
    public void Separator()
    {
        // Given
        var expression = new RepExpression(new TextExpression("x"), new TextExpression("y"), new RepCount(0, -1));

        // When
        var result = expression.Transform("xyxyxyx");

        // Then
        Assert.Equal("xyxyxyx", result);
    }

    // TODO: More tests.
}
