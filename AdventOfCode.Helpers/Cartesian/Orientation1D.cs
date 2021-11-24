using System;
namespace AdventOfCode.Helpers.Cartesian
{
    public record Orientation1D
    {
        public static readonly Orientation1D Standard = new(Vector1D.LeftToRight);

        private readonly int xx;

        public Vector1D XAxis => new(xx);

        public int Determinant => xx;

        private Orientation1D(int xx)
        {
            this.xx = xx;
        }

        public Orientation1D(Vector1D xAxis)
            : this((int)xAxis.X)
        {
            if (Determinant is not (1 or -1))
            {
                throw new ArgumentException("Must be an orthonormal basis.");
            }
        }

        public Orientation1D Inverse() => this;

        public static Orientation1D operator *(Orientation1D left, Orientation1D right) => new(
            left.xx * right.xx);

        public static Vector1D operator *(Orientation1D left, Vector1D right) => new(
            left.XAxis * right);

        public static Orientation1D operator -(Orientation1D value) => new(
            -value.xx);
    }
}