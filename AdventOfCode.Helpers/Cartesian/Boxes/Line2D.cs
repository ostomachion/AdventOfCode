namespace AdventOfCode.Helpers.Cartesian.Boxes;

public record Line2D(Interval I, long J = 0)
{
    public static readonly Line2D Infinite = new(Interval.Infinite);

    public Orientation2D Orientation { get; init; } = Orientation2D.Standard;

    public Point2D IStart => new(I.Start, J) { Orientation = Orientation };
    public Point2D IEnd => new(-I.End, -J) { Orientation = -Orientation };

    public Point2D[] Vertices => new[]
    {
            IStart, IEnd
        };

    public long Length => I.Length;

    public bool Contains(Coordinate2D coordinate)
    {
        coordinate = Loop(coordinate);
        return I.Contains(coordinate.X) && J == coordinate.Y;
    }

    public Coordinate2D Loop(Coordinate2D coordinate) => coordinate with
    {
        X = I.Looping ? I.Loop(coordinate.X) : coordinate.X,
    };

    public override string ToString() => $"{IStart}-{IEnd}";
}
