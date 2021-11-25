namespace Kleene.Tests;

public class ReqExpressionTests
{
    [Fact]
    public void Match()
    {
        // Given
        var expression = new ReqExpression(new AltExpression(new Expression[]
        {
            new TextExpression("x"),
            new PassExpression()
        }));

        // When
        var result = expression.Transform("x");

        // Then
        Assert.Equal("x", result);
    }

    [Fact]
    public void Empty()
    {
        // Given
        var expression = new ReqExpression(new AltExpression(new Expression[]
        {
            new TextExpression("x"),
            new PassExpression()
        }));

        // When
        var result = expression.Transform("");

        // Then
        Assert.Null(result);
    }
}
