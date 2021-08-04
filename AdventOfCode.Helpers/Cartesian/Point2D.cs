using System;

namespace AdventOfCode.Helpers.Cartesian
{
    public struct Point2D
    {
        public static readonly Point2D O = new(0, 0);

        public static readonly Point2D XPos = new(1, 0);
        public static readonly Point2D XNeg = new(-1, 0);
        public static readonly Point2D YPos = new(0, 1);
        public static readonly Point2D YNeg = new(0, -1);
        
        public static readonly Point2D N = new(0, -1);
        public static readonly Point2D NE = new(1, -1);
        public static readonly Point2D E = new(1, 0);
        public static readonly Point2D SE = new(1, 1);
        public static readonly Point2D S = new(0, 1);
        public static readonly Point2D SW = new(-1, 1);
        public static readonly Point2D W = new(-1, 0);
        public static readonly Point2D NW = new(-1, -1);

        public long X { get; set; }
        public long Y { get; set; }

        public long Norm => X * X + Y * Y;

        public Point2D(long x, long y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out long x, out long y)
        {
            x = X;
            y = Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Point2D d &&
                   X == d.X &&
                   Y == d.Y;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string? ToString() => $"({X},{Y})";

        public static bool operator ==(Point2D left, Point2D right) => left.Equals(right);

        public static bool operator !=(Point2D left, Point2D right) => !(left == right);

        public static Point2D operator +(Point2D left, Point2D right) => new(left.X + right.X, left.Y + right.Y);
        public static Point2D operator -(Point2D left, Point2D right) => new(left.X - right.X, left.Y - right.Y);
        public static Point2D operator *(Point2D left, long right) => new(left.X * right, left.Y * right);
        public static Point2D operator *(long left, Point2D right) => new(left * right.X, left * right.Y);
        public static Point2D operator /(Point2D left, long right) => new(left.X / right, left.Y / right);
        public static Point2D operator -(Point2D value) => new(-value.X, -value.Y);

        public static long operator *(Point2D left, Point2D right) => left.X * right.X + left.Y * right.Y;
    }
}