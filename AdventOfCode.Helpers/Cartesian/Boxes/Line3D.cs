using System;

namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Line3D(Interval I, long J = 0, long K = 0)
    {
        public Orientation3D Orientation { get; init; } = Orientation3D.Standard;

        public Point3D IStart => new(I.Start, J, K) { Orientation = Orientation };
        public Point3D IEnd => new(-I.End, -J, -K) { Orientation = -Orientation };

        public Point3D[] Vertices => new []
        {
            IStart, IEnd
        };

        public long Length => I.Length;

        public override string ToString() => $"{IStart}-{IEnd}";
    }
}