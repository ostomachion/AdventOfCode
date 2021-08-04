using System;

namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Line2D(Interval I, long J = 0)
    {
        public Orientation2D Orientation { get; init; } = Orientation2D.Standard;

        public Point2D IStart => new(I.Start, J) { Orientation = Orientation };
        public Point2D IEnd => new(-I.End, -J) { Orientation = -Orientation };

        public Point2D[] Vertices => new[]
        {
            IStart, IEnd
        };

        public long Length => I.Length;

        public override string ToString() => $"{IStart}-{IEnd}";
    }
}