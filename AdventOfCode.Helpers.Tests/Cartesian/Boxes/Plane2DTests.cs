using System.Linq;
using AdventOfCode.Helpers.Cartesian;
using AdventOfCode.Helpers.Cartesian.Boxes;
using Xunit;

namespace AdventOfCode.Helpers.Tests.Cartesian.Boxes
{
    public class Plane2DTests
    {
        [Fact]
        public void Test()
        {
            var plane = new Plane2D(new(1, 2), new(3, 4));
            var edges = plane.Edges.ToList();
            Assert.Collection(edges,
                item => {
                    Assert.Equal(Vector2D.LeftToRight, item.Orientation.XAxis);
                    Assert.Equal(1, item.Orientation.Determinant);

                    Assert.Equal(1, item.IStart.X);
                    Assert.Equal(3, item.IStart.Y);
                    Assert.Equal(2, item.IEnd.X);
                    Assert.Equal(3, item.IEnd.Y);
                },
                item => {
                    Assert.Equal(Vector2D.BottomToTop, item.Orientation.XAxis);
                    Assert.Equal(1, item.Orientation.Determinant);

                    Assert.Equal(2, item.IStart.X);
                    Assert.Equal(3, item.IStart.Y);
                    Assert.Equal(2, item.IEnd.X);
                    Assert.Equal(4, item.IEnd.Y);
                },
                item => {
                    Assert.Equal(Vector2D.RightToLeft, item.Orientation.XAxis);
                    Assert.Equal(1, item.Orientation.Determinant);

                    Assert.Equal(2, item.IStart.X);
                    Assert.Equal(4, item.IStart.Y);
                    Assert.Equal(1, item.IEnd.X);
                    Assert.Equal(4, item.IEnd.Y);
                },
                item => {
                    Assert.Equal(Vector2D.TopToBottom, item.Orientation.XAxis);
                    Assert.Equal(1, item.Orientation.Determinant);
                    
                    Assert.Equal(1, item.IStart.X);
                    Assert.Equal(4, item.IStart.Y);
                    Assert.Equal(1, item.IEnd.X);
                    Assert.Equal(3, item.IEnd.Y);
                }
            );
        }
    }
}