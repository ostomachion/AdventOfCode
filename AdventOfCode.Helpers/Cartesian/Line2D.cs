using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers.Cartesian
{
    public struct Line2D
    {
        public Dimension XLength { get; set; }
        public long Y { get; set; }
        public Orientation2D Orientation { get; set; }

        public Point2D VertexXStart => Orientation * new Point2D(XLength.Start!.Value, Y);
        public Point2D VertexXEnd => Orientation * new Point2D(XLength.End!.Value, Y);

        public long Length => XLength.Length!.Value;

        public Line2D(Dimension xLength)
        {
            XLength = xLength;
            Y = 0;
            Orientation = Orientation2D.I;
        }

        public Line2D(Dimension xLength, long y, Orientation2D orientation)
        {
            XLength = xLength;
            Y = y;
            Orientation = orientation;
        }

        public override bool Equals(object? obj)
        {
            return obj is Line2D d &&
                   EqualityComparer<Dimension>.Default.Equals(XLength, d.XLength) &&
                   EqualityComparer<long>.Default.Equals(Y, d.Y) &&
                   EqualityComparer<Orientation2D>.Default.Equals(Orientation, d.Orientation);
        }

        public override int GetHashCode() => HashCode.Combine(XLength, Y, Orientation);

        public static bool operator ==(Line2D left, Line2D right) => left.Equals(right);

        public static bool operator !=(Line2D left, Line2D right) => !(left == right);
    }
}