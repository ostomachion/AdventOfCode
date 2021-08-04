using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers.Cartesian
{
    public struct Plane3D
    {
        public Dimension XLength { get; set; }
        public Dimension YLength { get; set; }
        public long Z { get; set; }
        public Orientation3D Orientation { get; set; }

        public Line3D EdgeXStart => throw new NotImplementedException();
        public Line3D EdgeXEnd => throw new NotImplementedException();
        public Line3D EdgeYStart => throw new NotImplementedException();
        public Line3D EdgeYEnd => throw new NotImplementedException();

        public long Perimeter => 2 * (EdgeXStart.Length + EdgeYStart.Length);
        public long? Area => EdgeXStart.Length * EdgeYStart.Length;

        public Plane3D(Dimension xLength, Dimension yLength)
        {
            XLength = xLength;
            YLength = yLength;
            Z = 0;
            Orientation = Orientation3D.I;
        }

        public Plane3D(Dimension xLength, Dimension yLength, long z, Orientation3D orientation)
        {
            XLength = xLength;
            YLength = yLength;
            Z = z;
            Orientation = orientation;
        }

        public override bool Equals(object? obj)
        {
            return obj is Plane3D d &&
                   EqualityComparer<Dimension>.Default.Equals(XLength, d.XLength) &&
                   EqualityComparer<Dimension>.Default.Equals(YLength, d.YLength) &&
                   EqualityComparer<long>.Default.Equals(Z, d.Z) &&
                   EqualityComparer<Orientation3D>.Default.Equals(Orientation, d.Orientation);
        }

        public override int GetHashCode() => HashCode.Combine(XLength, YLength, Z, Orientation);

        public static bool operator ==(Plane3D left, Plane3D right) => left.Equals(right);

        public static bool operator !=(Plane3D left, Plane3D right) => !(left == right);
    }
}