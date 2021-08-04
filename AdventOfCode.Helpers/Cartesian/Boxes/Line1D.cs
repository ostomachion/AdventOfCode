using System;

namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Line1D(Interval ILength)
    {
        public Orientation1D Orientation { get; init; } = Orientation1D.Standard;

        public Point1D IStart => throw new NotImplementedException();
        public Point1D IEnd => throw new NotImplementedException();

        public Point1D[] Vertices => new[]
        {
            IStart, IEnd
        };

        public long Length => ILength.Length;

        public override string ToString() => $"{IStart}-{IEnd}";
    }
}