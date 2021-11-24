using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers.Cartesian
{
    public record Vector3D(long X, long Y, long Z)
    {
        public static readonly Vector3D O = new(0, 0, 0);
        public static readonly Vector3D I = new(1, 0, 0);
        public static readonly Vector3D J = new(0, 1, 0);
        public static readonly Vector3D K = new(0, 0, 1);

        public static readonly Vector3D LeftToRight = I;
        public static readonly Vector3D RightToLeft = -I;
        public static readonly Vector3D BottomToTop = J;
        public static readonly Vector3D TopToBottom = -J;
        public static readonly Vector3D FarToNear = K;
        public static readonly Vector3D NearToFar = -K;

        public static Vector3D FromChar(char c) => c switch
        {
            'E' or 'e' => LeftToRight,
            'W' or 'w' => RightToLeft,
            'N' or 'n' => BottomToTop,
            'S' or 's' => TopToBottom,
            'U' or 'u' => FarToNear,
            'D' or 'd' => NearToFar,
            _ => O
        };

        public static IEnumerable<Vector3D> FromString(string s) => s.Select(FromChar);

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

        public static Vector3D operator *(Vector3D left, long right) => new(
            left.X * right,
            left.Y * right,
            left.Z * right);

        public static long operator *(Vector3D left, Vector3D right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z;

        public static implicit operator Vector3D(Coordinate3D value) => new(
            value.X,
            value.Y,
            value.Z);

        public static implicit operator Coordinate3D(Vector3D value) => new(
            value.X,
            value.Y,
            value.Z);
    }
}