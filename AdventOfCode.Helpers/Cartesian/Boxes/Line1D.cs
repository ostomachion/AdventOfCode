using System;

namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Line1D(Interval I)
    {
        public Orientation1D Orientation { get; init; } = Orientation1D.Standard;

        public Point1D IStart => new(I.Start) { Orientation = Orientation };
        public Point1D IEnd => new(-I.Start) { Orientation = -Orientation };

        public Point1D[] Vertices => new[]
        {
            IStart, IEnd
        };

        public long Length => I.Length;

        public override string ToString() => $"{IStart}-{IEnd}";
    }
}