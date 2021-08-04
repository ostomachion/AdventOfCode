using AdventOfCode.Helpers.Cartesian.Boxes;

namespace AdventOfCode.Helpers.Cartesian
{
    public record Vector1D(long X)
    {
        public static readonly Vector1D O = new (0);
        public static readonly Vector1D I = new (1);
        public static readonly Vector1D In = new (-1);

        public static readonly Vector1D LeftToRight = I;
        public static readonly Vector1D RightToLeft = -I;

        public static Vector1D operator +(Vector1D left, Vector1D right) => new(
            left.X + right.X);

        public static Vector1D operator -(Vector1D left, Vector1D right) => new(
            left.X - right.X);

        public static Vector1D operator -(Vector1D value) => new(
            -value.X);
            
        public static long operator *(Vector1D left, Vector1D right) =>
            left.X * right.X;
    }
}