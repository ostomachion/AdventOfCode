using AdventOfCode.Helpers.Cartesian;
using AdventOfCode.Helpers.Cartesian.Boxes;
using Xunit;

namespace AdventOfCode.Helpers.Tests.Cartesian.Boxes;

public class Line3DTests
{
    [Fact]
    public void Vertices()
    {
        var line = new Line3D(new(1, 2), 3, 4);
        var vertices = line.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector3D.LeftToRight, item.Orientation.XAxis);
                Assert.Equal(Vector3D.BottomToTop, item.Orientation.YAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(1, item.X);
                Assert.Equal(3, item.Y);
                Assert.Equal(4, item.Z);
            },
            item =>
            {
                Assert.Equal(Vector3D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(Vector3D.TopToBottom, item.Orientation.YAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(2, item.X);
                Assert.Equal(3, item.Y);
                Assert.Equal(4, item.Z);
            }
        );
    }

    [Fact]
    public void VerticesRotated()
    {
        var line = new Line3D(new(1, 2), 3, 4) { Orientation = new(Vector3D.BottomToTop, Vector3D.FarToNear, Vector3D.LeftToRight) };
        var vertices = line.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector3D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(Vector3D.FarToNear, item.Orientation.YAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(4, item.X);
                Assert.Equal(1, item.Y);
                Assert.Equal(3, item.Z);
            },
            item =>
            {
                Assert.Equal(Vector3D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(Vector3D.NearToFar, item.Orientation.YAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(4, item.X);
                Assert.Equal(2, item.Y);
                Assert.Equal(3, item.Z);
            }
        );
    }

    [Fact]
    public void VerticesFlipped()
    {
        var line = new Line3D(new(1, 2), 3, 4) { Orientation = new(Vector3D.BottomToTop, Vector3D.FarToNear, Vector3D.RightToLeft) };
        var vertices = line.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector3D.BottomToTop, item.Orientation.XAxis);
                Assert.Equal(Vector3D.FarToNear, item.Orientation.YAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(-4, item.X);
                Assert.Equal(1, item.Y);
                Assert.Equal(3, item.Z);
            },
            item =>
            {
                Assert.Equal(Vector3D.TopToBottom, item.Orientation.XAxis);
                Assert.Equal(Vector3D.NearToFar, item.Orientation.YAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(-4, item.X);
                Assert.Equal(2, item.Y);
                Assert.Equal(3, item.Z);
            }
        );
    }
}
