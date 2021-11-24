using AdventOfCode.Helpers.Cartesian;
using AdventOfCode.Helpers.Cartesian.Boxes;

namespace AdventOfCode.Helpers.Tests.Cartesian.Boxes;

public class Plane2DTests
{
    [Fact]
    public void Edges()
    {
        var plane = new Plane2D(new(1, 2), new(3, 4));
        var edges = plane.Edges.ToList();
        Assert.Collection(edges,
            item =>
            {
                Assert.Equal(Vector2D.LeftToRight, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(1, item.IStart.X);
                Assert.Equal(3, item.IStart.Y);
                Assert.Equal(2, item.IEnd.X);
                Assert.Equal(3, item.IEnd.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(2, item.IStart.X);
                Assert.Equal(3, item.IStart.Y);
                Assert.Equal(2, item.IEnd.X);
                Assert.Equal(4, item.IEnd.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(2, item.IStart.X);
                Assert.Equal(4, item.IStart.Y);
                Assert.Equal(1, item.IEnd.X);
                Assert.Equal(4, item.IEnd.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(1, item.IStart.X);
                Assert.Equal(4, item.IStart.Y);
                Assert.Equal(1, item.IEnd.X);
                Assert.Equal(3, item.IEnd.Y);
            }
        );
    }

    [Fact]
    public void EdgesRotated()
    {
        var plane = new Plane2D(new(1, 2), new(3, 4)) { Orientation = new(Vector2D.TopToBottom, Vector2D.LeftToRight) };
        var edges = plane.Edges.ToList();
        Assert.Collection(edges,
            item =>
            {
                Assert.Equal(Vector2D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(3, item.IStart.X);
                Assert.Equal(-1, item.IStart.Y);
                Assert.Equal(3, item.IEnd.X);
                Assert.Equal(-2, item.IEnd.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.LeftToRight, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(3, item.IStart.X);
                Assert.Equal(-2, item.IStart.Y);
                Assert.Equal(4, item.IEnd.X);
                Assert.Equal(-2, item.IEnd.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(4, item.IStart.X);
                Assert.Equal(-2, item.IStart.Y);
                Assert.Equal(4, item.IEnd.X);
                Assert.Equal(-1, item.IEnd.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(4, item.IStart.X);
                Assert.Equal(-1, item.IStart.Y);
                Assert.Equal(3, item.IEnd.X);
                Assert.Equal(-1, item.IEnd.Y);
            }
        );
    }

    [Fact]
    public void EdgesFlipped()
    {
        var plane = new Plane2D(new(1, 2), new(3, 4)) { Orientation = new(Vector2D.LeftToRight, Vector2D.TopToBottom) };
        var edges = plane.Edges.ToList();
        Assert.Collection(edges,
            item =>
            {
                Assert.Equal(Vector2D.LeftToRight, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(1, item.IStart.X);
                Assert.Equal(-3, item.IStart.Y);
                Assert.Equal(2, item.IEnd.X);
                Assert.Equal(-3, item.IEnd.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(2, item.IStart.X);
                Assert.Equal(-3, item.IStart.Y);
                Assert.Equal(2, item.IEnd.X);
                Assert.Equal(-4, item.IEnd.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(2, item.IStart.X);
                Assert.Equal(-4, item.IStart.Y);
                Assert.Equal(1, item.IEnd.X);
                Assert.Equal(-4, item.IEnd.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(1, item.IStart.X);
                Assert.Equal(-4, item.IStart.Y);
                Assert.Equal(1, item.IEnd.X);
                Assert.Equal(-3, item.IEnd.Y);
            }
        );
    }

    [Fact]
    public void Vertices()
    {
        var plane = new Plane2D(new(1, 2), new(3, 4));
        var vertices = plane.Vertices.ToList();
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
                Assert.Equal(Vector2D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(2, item.X);
                Assert.Equal(3, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(2, item.X);
                Assert.Equal(4, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(1, item.X);
                Assert.Equal(4, item.Y);
            }
        );
    }

    [Fact]
    public void VerticesRotated()
    {
        var plane = new Plane2D(new(1, 2), new(3, 4)) { Orientation = new(Vector2D.TopToBottom, Vector2D.LeftToRight) };
        var vertices = plane.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector2D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(3, item.X);
                Assert.Equal(-1, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.LeftToRight, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(3, item.X);
                Assert.Equal(-2, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(4, item.X);
                Assert.Equal(-2, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(4, item.X);
                Assert.Equal(-1, item.Y);
            }
        );
    }

    [Fact]
    public void VerticesFlipped()
    {
        var plane = new Plane2D(new(1, 2), new(3, 4)) { Orientation = new(Vector2D.LeftToRight, Vector2D.TopToBottom) };
        var vertices = plane.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector2D.LeftToRight, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(1, item.X);
                Assert.Equal(-3, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(2, item.X);
                Assert.Equal(-3, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(2, item.X);
                Assert.Equal(-4, item.Y);
            },
            item =>
            {
                Assert.Equal(Vector2D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(1, item.X);
                Assert.Equal(-4, item.Y);
            }
        );
    }
}
