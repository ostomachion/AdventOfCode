namespace AdventOfCode.Helpers.Cartesian.Boxes;

public record Plane3D(Interval I, Interval J, long K = 0)
{
    public static readonly Plane3D Infinite = new(Interval.Infinite, Interval.Infinite);

    public Orientation3D Orientation { get; init; } = Orientation3D.Standard;

    public Line3D IStart => new(-J, I.Start, -K) { Orientation = new Orientation3D(Vector3D.TopToBottom, Vector3D.LeftToRight, Vector3D.NearToFar) * Orientation };
    public Line3D IEnd => new(J, -I.End, K) { Orientation = new Orientation3D(Vector3D.BottomToTop, Vector3D.RightToLeft, Vector3D.FarToNear) * Orientation };
    public Line3D JStart => new(I, J.Start, K) { Orientation = Orientation3D.Standard * Orientation };
    public Line3D JEnd => new(-I, -J.End, -K) { Orientation = -Orientation3D.Standard * Orientation };

    public Line3D[] Edges => new[]
    {
        JStart, IEnd,
        JEnd, IStart
    };

    public Point3D[] Vertices => new[]
    {
        JStart.IStart, IEnd.IStart,
        JEnd.IStart, IStart.IStart
    };

    public long Perimeter => 2 * (IStart.Length + JStart.Length);
    public long Area => I.Length * J.Length;

    public bool Contains(Coordinate3D coordinate)
    {
        coordinate = Loop(coordinate);
        return I.Contains(coordinate.X) && J.Contains(coordinate.Y) && K == coordinate.Z;
    }

    public Coordinate3D Loop(Coordinate3D coordinate) => coordinate with
    {
        X = I.Looping ? I.Loop(coordinate.X) : coordinate.X,
        Y = J.Looping ? J.Loop(coordinate.Y) : coordinate.Y,
    };

    public override string ToString() => $"{IStart.IStart}-{IEnd.IEnd}";
}
