using AdventOfCode.Helpers.Cartesian.Boxes;

namespace AdventOfCode.Helpers.Cartesian
{
    public record Vector3D(long X, long Y, long Z)
    {
        public static readonly Vector3D O = new (0, 0, 0);
        public static readonly Vector3D I = new (1, 0, 0);
        public static readonly Vector3D J = new (0, 1, 0);
        public static readonly Vector3D K = new (0, 0, 1);

        public static readonly Vector3D LeftToRight = I;
        public static readonly Vector3D RightToLeft = -I;
        public static readonly Vector3D BottomToTop = J;
        public static readonly Vector3D TopToBottom = -J;
        public static readonly Vector3D FrontToBack = K;
        public static readonly Vector3D BackToFront = -K;

        public static Vector3D operator +(Vector3D left, Vector3D right) => new(
            left.X + right.X,
            left.Y + right.Y,
            left.Z + right.Z);

        public static Vector3D operator -(Vector3D left, Vector3D right) => new(
            left.X - right.X,
            left.Y - right.Y,
            left.Z - right.Z);

        public static Vector3D operator -(Vector3D value) => new(
            -value.X,
            -value.Y,
            -value.Z);
            
        public static long operator *(Vector3D left, Vector3D right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z;
    }
}