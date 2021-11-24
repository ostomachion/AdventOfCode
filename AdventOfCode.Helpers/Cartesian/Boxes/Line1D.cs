namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Line1D(Interval I)
    {
        public static readonly Line1D Infinite = new(Interval.Infinite);

        public Orientation1D Orientation { get; init; } = Orientation1D.Standard;

        public Point1D IStart => new(I.Start) { Orientation = Orientation };
        public Point1D IEnd => new(-I.End) { Orientation = -Orientation };

        public Point1D[] Vertices => new[]
        {
            IStart, IEnd
        };

        public long Length => I.Length;

        public bool Contains(Coordinate1D coordinate)
        {
            coordinate = Loop(coordinate);
            return I.Contains(coordinate.X);
        }

        public Coordinate1D Loop(Coordinate1D coordinate) => coordinate with
        {
            X = I.Looping ? I.Loop(coordinate.X) : coordinate.X,
        };

        public override string ToString() => $"{IStart}-{IEnd}";
    }
}