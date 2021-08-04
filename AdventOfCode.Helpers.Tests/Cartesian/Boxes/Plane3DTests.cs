using System.Numerics;
using System.Linq;
using AdventOfCode.Helpers.Cartesian;
using AdventOfCode.Helpers.Cartesian.Boxes;
using Xunit;

namespace AdventOfCode.Helpers.Tests.Cartesian.Boxes
{
    public class Plane3DTests
    {
        [Fact]
        public void Edges()
        {
            var plane = new Plane3D(new(1, 2), new(3, 4), 5);
            var edges = plane.Edges.ToList();
            Assert.Collection(edges,
                item =>
                {
                    Assert.Equal(Vector3D.LeftToRight, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.BottomToTop, item.Orientation.YAxis);
                    Assert.Equal(1, item.Orientation.Determinant);

                    Assert.Equal(1, item.IStart.X);
                    Assert.Equal(3, item.IStart.Y);
                    Assert.Equal(5, item.IStart.Z);
                    Assert.Equal(2, item.IEnd.X);
                    Assert.Equal(3, item.IEnd.Y);
                    Assert.Equal(5, item.IEnd.Z);
                },
                item =>
                {
                    Assert.Equal(Vector3D.BottomToTop, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.RightToLeft, item.Orientation.YAxis);
                    Assert.Equal(1, item.Orientation.Determinant);

                    Assert.Equal(2, item.IStart.X);
                    Assert.Equal(3, item.IStart.Y);
                    Assert.Equal(5, item.IStart.Z);
                    Assert.Equal(2, item.IEnd.X);
                    Assert.Equal(4, item.IEnd.Y);
                    Assert.Equal(5, item.IEnd.Z);
                },
                item =>
                {
                    Assert.Equal(Vector3D.RightToLeft, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.TopToBottom, item.Orientation.YAxis);
                    Assert.Equal(-1, item.Orientation.Determinant);

                    Assert.Equal(2, item.IStart.X);
                    Assert.Equal(4, item.IStart.Y);
                    Assert.Equal(5, item.IStart.Z);
                    Assert.Equal(1, item.IEnd.X);
                    Assert.Equal(4, item.IEnd.Y);
                    Assert.Equal(5, item.IEnd.Z);
                },
                item =>
                {
                    Assert.Equal(Vector3D.TopToBottom, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.LeftToRight, item.Orientation.YAxis);
                    Assert.Equal(-1, item.Orientation.Determinant);

                    Assert.Equal(1, item.IStart.X);
                    Assert.Equal(4, item.IStart.Y);
                    Assert.Equal(5, item.IStart.Z);
                    Assert.Equal(1, item.IEnd.X);
                    Assert.Equal(3, item.IEnd.Y);
                    Assert.Equal(5, item.IEnd.Z);
                }
            );
        }

        [Fact]
        public void EdgesRotated()
        {
            var plane = new Plane3D(new(1, 2), new(3, 4), 5) { Orientation = new(Vector3D.NearToFar, Vector3D.TopToBottom, Vector3D.RightToLeft) };
            var edges = plane.Edges.ToList();
            Assert.Collection(edges,
                item =>
                {
                    Assert.Equal(Vector3D.NearToFar, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.TopToBottom, item.Orientation.YAxis);
                    Assert.Equal(1, item.Orientation.Determinant);

                    Assert.Equal(-5, item.IStart.X);
                    Assert.Equal(-3, item.IStart.Y);
                    Assert.Equal(-1, item.IStart.Z);
                    Assert.Equal(-5, item.IEnd.X);
                    Assert.Equal(-3, item.IEnd.Y);
                    Assert.Equal(-2, item.IEnd.Z);
                },
                item =>
                {
                    Assert.Equal(Vector3D.TopToBottom, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.FarToNear, item.Orientation.YAxis);
                    Assert.Equal(1, item.Orientation.Determinant);

                    Assert.Equal(-5, item.IStart.X);
                    Assert.Equal(-3, item.IStart.Y);
                    Assert.Equal(-2, item.IStart.Z);
                    Assert.Equal(-5, item.IEnd.X);
                    Assert.Equal(-4, item.IEnd.Y);
                    Assert.Equal(-2, item.IEnd.Z);
                },
                item =>
                {
                    Assert.Equal(Vector3D.FarToNear, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.BottomToTop, item.Orientation.YAxis);
                    Assert.Equal(-1, item.Orientation.Determinant);

                    Assert.Equal(-5, item.IStart.X);
                    Assert.Equal(-4, item.IStart.Y);
                    Assert.Equal(-2, item.IStart.Z);
                    Assert.Equal(-5, item.IEnd.X);
                    Assert.Equal(-4, item.IEnd.Y);
                    Assert.Equal(-1, item.IEnd.Z);
                },
                item =>
                {
                    Assert.Equal(Vector3D.BottomToTop, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.NearToFar, item.Orientation.YAxis);
                    Assert.Equal(-1, item.Orientation.Determinant);

                    Assert.Equal(-5, item.IStart.X);
                    Assert.Equal(-4, item.IStart.Y);
                    Assert.Equal(-1, item.IStart.Z);
                    Assert.Equal(-5, item.IEnd.X);
                    Assert.Equal(-3, item.IEnd.Y);
                    Assert.Equal(-1, item.IEnd.Z);
                }
            );
        }

        [Fact]
        public void EdgesFlipped()
        {
            var plane = new Plane3D(new(1, 2), new(3, 4), 5) { Orientation = new(Vector3D.FarToNear, Vector3D.TopToBottom, Vector3D.RightToLeft) };
            var edges = plane.Edges.ToList();
            Assert.Collection(edges,
                item =>
                {
                    Assert.Equal(Vector3D.FarToNear, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.TopToBottom, item.Orientation.YAxis);
                    Assert.Equal(-1, item.Orientation.Determinant);

                    Assert.Equal(-5, item.IStart.X);
                    Assert.Equal(-3, item.IStart.Y);
                    Assert.Equal(1, item.IStart.Z);
                    Assert.Equal(-5, item.IEnd.X);
                    Assert.Equal(-3, item.IEnd.Y);
                    Assert.Equal(2, item.IEnd.Z);
                },
                item =>
                {
                    Assert.Equal(Vector3D.TopToBottom, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.NearToFar, item.Orientation.YAxis);
                    Assert.Equal(-1, item.Orientation.Determinant);

                    Assert.Equal(-5, item.IStart.X);
                    Assert.Equal(-3, item.IStart.Y);
                    Assert.Equal(2, item.IStart.Z);
                    Assert.Equal(-5, item.IEnd.X);
                    Assert.Equal(-4, item.IEnd.Y);
                    Assert.Equal(2, item.IEnd.Z);
                },
                item =>
                {
                    Assert.Equal(Vector3D.NearToFar, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.BottomToTop, item.Orientation.YAxis);
                    Assert.Equal(1, item.Orientation.Determinant);

                    Assert.Equal(-5, item.IStart.X);
                    Assert.Equal(-4, item.IStart.Y);
                    Assert.Equal(2, item.IStart.Z);
                    Assert.Equal(-5, item.IEnd.X);
                    Assert.Equal(-4, item.IEnd.Y);
                    Assert.Equal(1, item.IEnd.Z);
                },
                item =>
                {
                    Assert.Equal(Vector3D.BottomToTop, item.Orientation.XAxis);
                    Assert.Equal(Vector3D.FarToNear, item.Orientation.YAxis);
                    Assert.Equal(1, item.Orientation.Determinant);

                    Assert.Equal(-5, item.IStart.X);
                    Assert.Equal(-4, item.IStart.Y);
                    Assert.Equal(1, item.IStart.Z);
                    Assert.Equal(-5, item.IEnd.X);
                    Assert.Equal(-3, item.IEnd.Y);
                    Assert.Equal(1, item.IEnd.Z);
                }
            );
        }
    }
}