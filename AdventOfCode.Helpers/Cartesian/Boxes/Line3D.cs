namespace AdventOfCode.Helpers.Cartesian.Boxes;

public record Line3D(Interval I, long J = 0, long K = 0)
{
    public static readonly Line3D Infinite = new(Interval.Infinite);

    public Orientation3D Orientation { get; init; } = Orientation3D.Standard;

    public Point3D IStart => new(I.Start, J, K) { Orientation = Orientation };
    public Point3D IEnd => new(-I.End, -J, -K) { Orientation = -Orientation };

    public Point3D[] Vertices => new[]
    {
        IStart, IEnd
    };

    public long Length => I.Length;

    public bool Contains(Coordinate3D coordinate)
    {
        coordinate = Loop(coordinate);
        return I.Contains(coordinate.X) && J == coordinate.Y && K == coordinate.Z;
    }

    public Coordinate3D Loop(Coordinate3D coordinate) => coordinate with
    {
        X = I.Looping ? I.Loop(coordinate.X) : coordinate.X,
    };

    public override string ToString() => $"{IStart}-{IEnd}";
}
