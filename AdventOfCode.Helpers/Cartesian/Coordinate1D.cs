namespace AdventOfCode.Helpers.Cartesian;

public record Coordinate1D(long X) : ICoordinate
{
    public static readonly Coordinate1D O = new(0);
    public static readonly Coordinate1D I = new(1);

    public static Coordinate1D operator +(Coordinate1D left, Vector1D right) => new(
        left.X + right.X);

    public static Coordinate1D operator -(Coordinate1D left, Vector1D right) => new(
        left.X - right.X);

    public static Coordinate1D operator -(Coordinate1D value) => new(
        -value.X);

    public static Coordinate1D operator *(Coordinate1D left, long right) => new(
        left.X * right);
}
