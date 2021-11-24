using AdventOfCode.Helpers.Cartesian;
using AdventOfCode.Helpers.Cartesian.Boxes;
using Xunit;

namespace AdventOfCode.Helpers.Tests.Cartesian.Boxes;

public class Line2DTests
{
    [Fact]
    public void Vertices()
    {
        var line = new Line2D(new(1, 2), 3);
        var vertices = line.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector2D.LeftToRight, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(1, item.X);
                Assert.Equal(3, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(2, item.X);
                Assert.Equal(3, item.Y);
            }
        );
    }

    [Fact]
    public void VerticesRotated()
    {
        var line = new Line2D(new(1, 2), 3) { Orientation = new(Vector2D.BottomToTop, Vector2D.RightToLeft) };
        var vertices = line.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector2D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(-3, item.X);
                Assert.Equal(1, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(-3, item.X);
                Assert.Equal(2, item.Y);
            }
        );
    }

    [Fact]
    public void VerticesFlipped()
    {
        var line = new Line2D(new(1, 2), 3) { Orientation = new(Vector2D.BottomToTop, Vector2D.LeftToRight) };
        var vertices = line.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector2D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(3, item.X);
                Assert.Equal(1, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(3, item.X);
                Assert.Equal(2, item.Y);
            }
        );
    }
}
