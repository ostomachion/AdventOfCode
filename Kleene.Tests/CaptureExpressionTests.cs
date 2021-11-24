using System.Linq;
using Xunit;

namespace Kleene.Tests;

public class CaptureExpressionTests
{
    [Fact]
    public void Match()
    {
        // Given
        var expression = new CaptureExpression("foo", new TextExpression("x"));

        // When
        var context = new ExpressionContext("x");
        var result = expression.Run(context).FirstOrDefault();

        // Then
        Assert.Equal("x", result?.Input);
        Assert.Equal("x", result?.Output);
        Assert.Collection(context.CaptureTree.Current.Children,
            item =>
            {
                Assert.Equal("foo", item.Name);
                Assert.NotNull(item.Value);
                Assert.Equal("x", item.Value!.Input);
                Assert.Equal("x", item.Value!.Output);
                Assert.Empty(item.Children);
            }
        );
    }

    [Fact]
    public void Shadow()
    {
        // Given
        var expression = new ConcatExpression(new Expression[] {
                new CaptureExpression("foo", new TextExpression("x")),
                new CaptureExpression("foo", new TextExpression("y"))
            });

        // When
        var context = new ExpressionContext("xy");
        var result = expression.Run(context).FirstOrDefault();

        // Then
        Assert.Equal("xy", result?.Input);
        Assert.Equal("xy", result?.Output);
        Assert.Collection(context.CaptureTree.Current.Children,
            item =>
            {
                Assert.Equal("foo", item.Name);
                Assert.NotNull(item.Value);
                Assert.Equal("y", item.Value!.Input);
                Assert.Equal("y", item.Value!.Output);
                Assert.Empty(item.Children);
            },
            item =>
            {
                Assert.Equal("foo", item.Name);
                Assert.NotNull(item.Value);
                Assert.Equal("x", item.Value!.Input);
                Assert.Equal("x", item.Value!.Output);
                Assert.Empty(item.Children);
            }
        );
    }

    [Fact]
    public void Nested()
    {
        // Given
        var expression = new CaptureExpression("foo", new CaptureExpression("bar", new TextExpression("x")));

        // When
        var context = new ExpressionContext("x");
        var result = expression.Run(context).FirstOrDefault();

        // Then
        Assert.Equal("x", result?.Input);
        Assert.Equal("x", result?.Output);
        Assert.Collection(context.CaptureTree.Current.Children,
            item =>
            {
                Assert.Equal("foo", item.Name);
                Assert.NotNull(item.Value);
                Assert.Equal("x", item.Value!.Input);
                Assert.Equal("x", item.Value!.Output);
                Assert.Collection(item.Children,
                    item =>
                    {
                        Assert.Equal("bar", item.Name);
                        Assert.NotNull(item.Value);
                        Assert.Equal("x", item.Value!.Input);
                        Assert.Equal("x", item.Value!.Output);
                        Assert.Empty(item.Children);
                    }
                );
            }
        );
    }

    [Fact]
    public void NestedSugar()
    {
        // Given
        var expression = new CaptureExpression("foo.bar", new TextExpression("x"));

        // When
        var context = new ExpressionContext("x");
        var result = expression.Run(context).FirstOrDefault();

        // Then
        Assert.Equal("x", result?.Input);
        Assert.Equal("x", result?.Output);
        Assert.Collection(context.CaptureTree.Current.Children,
            item =>
            {
                Assert.Equal("foo", item.Name);
                Assert.NotNull(item.Value);
                Assert.Equal("x", item.Value!.Input);
                Assert.Equal("x", item.Value!.Output);
                Assert.Collection(item.Children,
                    item =>
                    {
                        Assert.Equal("bar", item.Name);
                        Assert.NotNull(item.Value);
                        Assert.Equal("x", item.Value!.Input);
                        Assert.Equal("x", item.Value!.Output);
                        Assert.Empty(item.Children);
                    }
                );
            }
        );
    }
}
