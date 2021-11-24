using AdventOfCode.Helpers.Cartesian;
using AdventOfCode.Helpers.Cartesian.Boxes;
using System.Linq;
using Xunit;

namespace AdventOfCode.Helpers.Tests.Cartesian.Boxes;

public class Line1DTests
{
    [Fact]
    public void Vertices()
    {
        var line = new Line1D(new(1, 2));
        var vertices = line.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector1D.LeftToRight, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(1, item.X);
            },
            item =>
            {
                Assert.Equal(Vector1D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(2, item.X);
            }
        );
    }

    [Fact]
    public void VerticesFlipped()
    {
        var line = new Line1D(new(1, 2)) { Orientation = new(Vector1D.RightToLeft) };
        var vertices = line.Vertices.ToList();
        Assert.Collection(vertices,
            item =>
            {
                Assert.Equal(Vector1D.RightToLeft, item.Orientation.XAxis);
                Assert.Equal(-1, item.Orientation.Determinant);

                Assert.Equal(-1, item.X);
            },
            item =>
            {
                Assert.Equal(Vector1D.LeftToRight, item.Orientation.XAxis);
                Assert.Equal(1, item.Orientation.Determinant);

                Assert.Equal(-2, item.X);
            }
        );
    }
}
