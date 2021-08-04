using System;

namespace AdventOfCode.Helpers.Cartesian
{
    public struct Point3D
    {
        public static readonly Point3D O = new(0, 0, 0);

        public static readonly Point3D XPos = new(1, 0, 0);
        public static readonly Point3D XNeg = new(-1, 0, 0);
        public static readonly Point3D YPos = new(0, 1, 0);
        public static readonly Point3D YNeg = new(0, -1, 0);
        public static readonly Point3D ZPos = new(0, 0, 1);
        public static readonly Point3D ZNeg = new(0, 0, -1);

        public static readonly Point3D U = new (0, -1, 0);
        public static readonly Point3D D = new (0, 1, 0);
        public static readonly Point3D L = new (-1, 0, 0);
        public static readonly Point3D R = new (1, 0, 0);
        public static readonly Point3D F = new (0, 0, 1);
        public static readonly Point3D B = new (0, 0, -1);

        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public long Norm => X * X + Y * Y + Z * Z;

        public Point3D(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Deconstruct(out long x, out long y, out long z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public override bool Equals(object? obj)
        {
            return obj is Point3D d &&
                   X == d.X &&
                   Y == d.Y &&
                   Z == d.Z;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        public override string? ToString() => $"({X},{Y},{Z})";

        public static bool operator ==(Point3D left, Point3D right) => left.Equals(right);

        public static bool operator !=(Point3D left, Point3D right) => !(left == right);

        public static Point3D operator +(Point3D left, Point3D right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        public static Point3D operator -(Point3D left, Point3D right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        public static Point3D operator *(Point3D left, long right) => new(left.X * right, left.Y * right, left.Z * right);
        public static Point3D operator *(long left, Point3D right) => new(left * right.X, left * right.Y, left * right.Z);
        public static Point3D operator /(Point3D left, long right) => new(left.X / right, left.Y / right, left.Z / right);
        public static Point3D operator -(Point3D value) => new(-value.X, -value.Y, -value.Z);

        public static long operator *(Point3D left, Point3D right) => left.X * right.X + left.Y * right.Y + left.Z * right.Z;
    }
}