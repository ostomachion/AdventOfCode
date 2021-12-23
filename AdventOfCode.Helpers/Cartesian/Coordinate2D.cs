namespace AdventOfCode.Helpers.Cartesian;

public record Coordinate2D(long X, long Y) : ICoordinate
{
    public static readonly Coordinate2D O = new(0, 0);
    public static readonly Coordinate2D I = new(1, 0);
    public static readonly Coordinate2D J = new(0, 1);

    public static Coordinate2D operator +(Coordinate2D left, Vector2D right) => new(
        left.X + right.X,
        left.Y + right.Y);

    public static Coordinate2D operator -(Coordinate2D left, Vector2D right) => new(
        left.X - right.X,
        left.Y - right.Y);

    public static Coordinate2D operator -(Coordinate2D value) => new(
        -value.X,
        -value.Y);

    public static Coordinate2D operator *(Coordinate2D left, long right) => new(
        left.X * right,
        left.Y * right);

    public double DistanceTo(Coordinate2D other) => Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
    public double ManhattanDistanceTo(Coordinate2D other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    public override string ToString() => $"({X},{Y})";
}
