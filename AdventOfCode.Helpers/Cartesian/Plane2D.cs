using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers.Cartesian
{
    public struct Plane2D
    {
        public Dimension XLength { get; set; }
        public Dimension YLength { get; set; }
        public Orientation2D Orientation { get; set; }

        public Line2D EdgeXStart => new(YLength, XLength.Start!.Value, Orientation2D.R3);
        public Line2D EdgeXEnd => new(YLength, -XLength.End!.Value, Orientation2D.R);
        public Line2D EdgeYStart => new(XLength, YLength.Start!.Value, Orientation2D.I);
        public Line2D EdgeYEnd => new(XLength, YLength.End!.Value, Orientation2D.R2);

        public long Perimeter => 2 * (EdgeXStart.Length + EdgeYStart.Length);
        public long? Area => EdgeXStart.Length * EdgeYStart.Length;

        public Plane2D(Dimension xLength, Dimension yLength)
        {
            XLength = xLength;
            YLength = yLength;
            Orientation = Orientation2D.I;
        }

        public Plane2D(Dimension xLength, Dimension yLength, Orientation2D orientation)
        {
            XLength = xLength;
            YLength = yLength;
            Orientation = orientation;
        }

        public override bool Equals(object? obj)
        {
            return obj is Plane2D d &&
                   EqualityComparer<Dimension>.Default.Equals(XLength, d.XLength) &&
                   EqualityComparer<Dimension>.Default.Equals(YLength, d.YLength) &&
                   EqualityComparer<Orientation2D>.Default.Equals(Orientation, d.Orientation);
        }

        public override int GetHashCode() => HashCode.Combine(XLength, YLength, Orientation);

        public static bool operator ==(Plane2D left, Plane2D right) => left.Equals(right);

        public static bool operator !=(Plane2D left, Plane2D right) => !(left == right);
    }
}