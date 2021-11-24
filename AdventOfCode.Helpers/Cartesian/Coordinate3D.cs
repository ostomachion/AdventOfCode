namespace AdventOfCode.Helpers.Cartesian;

public record Coordinate3D(long X, long Y, long Z)
{
    public static readonly Coordinate3D O = new(0, 0, 0);
    public static readonly Coordinate3D I = new(1, 0, 0);
    public static readonly Coordinate3D J = new(0, 1, 0);
    public static readonly Coordinate3D K = new(0, 0, 1);

    public static Coordinate3D operator +(Coordinate3D left, Vector3D right) => new(
        left.X + right.X,
        left.Y + right.Y,
        left.Z + right.Z);

    public static Coordinate3D operator -(Coordinate3D left, Vector3D right) => new(
        left.X - right.X,
        left.Y - right.Y,
        left.Z - right.Z);

    public static Coordinate3D operator -(Coordinate3D value) => new(
        -value.X,
        -value.Y,
        -value.Z);

    public static Coordinate3D operator *(Coordinate3D left, long right) => new(
        left.X * right,
        left.Y * right,
        left.Z * right);
}
