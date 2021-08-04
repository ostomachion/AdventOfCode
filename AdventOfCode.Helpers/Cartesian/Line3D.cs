using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers.Cartesian
{
    public struct Line3D
    {
        public Dimension XLength { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
        public Orientation3D Orientation { get; set; }

        public Point3D VertexXStart => Orientation * new Point3D(XLength.Start!.Value, Y, Z);
        public Point3D VertexXEnd => Orientation * new Point3D(XLength.End!.Value, Y, Z);

        public long Length => XLength.Length!.Value;

        public Line3D(Dimension xLength)
        {
            XLength = xLength;
            Y = 0;
            Z = 0;
            Orientation = Orientation3D.I;
        }

        public Line3D(Dimension xLength, long y, long z, Orientation3D orientation)
        {
            XLength = xLength;
            Y = y;
            Z = z;
            Orientation = orientation;
        }

        public override bool Equals(object? obj)
        {
            return obj is Line3D d &&
                   EqualityComparer<Dimension>.Default.Equals(XLength, d.XLength) &&
                   EqualityComparer<long>.Default.Equals(Y, d.Y) &&
                   EqualityComparer<long>.Default.Equals(Z, d.Z) &&
                   EqualityComparer<Orientation3D>.Default.Equals(Orientation, d.Orientation);
        }

        public override int GetHashCode() => HashCode.Combine(XLength, Y, Z, Orientation);

        public static bool operator ==(Line3D left, Line3D right) => left.Equals(right);

        public static bool operator !=(Line3D left, Line3D right) => !(left == right);
    }
}