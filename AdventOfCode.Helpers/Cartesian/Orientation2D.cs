using System;

namespace AdventOfCode.Helpers.Cartesian
{
    public struct Orientation2D
    {
        public static readonly Orientation2D I = new(1, 0, 0, 1);
        public static readonly Orientation2D R = new(0, 1, -1, 0);
        public static readonly Orientation2D R2 = R * R;
        public static readonly Orientation2D R3 = R * R * R;

        public int XX { get; }
        public int XY { get; }
        public int YX { get; }
        public int YY { get; }

        public Point2D XAxis => new(XX, XY);
        public Point2D YAxis => new(YX, YY);

        public Orientation2D(int xx, int xy, int yx, int yy)
        {
            XX = xx;
            XY = xy;
            YX = yx;
            YY = yy;
        }

        public Orientation2D(Point2D xAxis, Point2D yAxis)
        {
            XX = (int)xAxis.X;
            XY = (int)xAxis.Y;
            YX = (int)yAxis.X;
            YY = (int)yAxis.Y;
        }

        public static Orientation2D operator *(Orientation2D left, Orientation2D right)
        {
            return new Orientation2D(
                left.XX * right.XX + left.XY * right.YX,
                left.XX * right.XY + left.XY * right.YY,

                left.YX * right.XX + left.YY * right.YX,
                left.YX * right.XY + left.YY * right.YY);
        }

        public override bool Equals(object? obj)
        {
            return obj is Orientation2D d &&
                   XX == d.XX &&
                   XY == d.XY &&
                   YX == d.YX &&
                   YY == d.YY;
        }

        public override int GetHashCode() => HashCode.Combine(XX, XY, YX, YY);

        public static bool operator ==(Orientation2D left, Orientation2D right) => left.Equals(right);

        public static bool operator !=(Orientation2D left, Orientation2D right) => !(left == right);

        public static Point2D operator *(Orientation2D left, Point2D right)
        {
            return new(left.XAxis * right, left.YAxis * right);
        }

        public static Orientation2D operator -(Orientation2D value) => new(
            -value.XX, -value.XY,
            -value.YX, -value.YY);
    }
}