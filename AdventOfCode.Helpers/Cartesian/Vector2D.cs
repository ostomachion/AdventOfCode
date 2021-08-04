using AdventOfCode.Helpers.Cartesian.Boxes;

namespace AdventOfCode.Helpers.Cartesian
{
    public record Vector2D(long X, long Y)
    {
        public static readonly Vector2D O = new (0, 0);
        public static readonly Vector2D I = new (1, 0);
        public static readonly Vector2D J = new (0, 1);

        public static readonly Vector2D LeftToRight = I;
        public static readonly Vector2D RightToLeft = -I;
        public static readonly Vector2D BottomToTop = J;
        public static readonly Vector2D TopToBottom = -J;

        public static Vector2D operator +(Vector2D left, Vector2D right) => new(
            left.X + right.X,
            left.Y + right.Y);

        public static Vector2D operator -(Vector2D left, Vector2D right) => new(
            left.X - right.X,
            left.Y - right.Y);

        public static Vector2D operator -(Vector2D value) => new(
            -value.X,
            -value.Y);
            
        public static long operator *(Vector2D left, Vector2D right) =>
            left.X * right.X +
            left.Y * right.Y;
    }
}