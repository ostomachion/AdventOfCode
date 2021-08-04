using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Plane3D(Interval I, Interval J, long Z = 0)
    {
        public Orientation3D Orientation { get; init; } = Orientation3D.Standard;

        public Line3D IStart => throw new NotImplementedException();
        public Line3D IEnd => throw new NotImplementedException();
        public Line3D JStart => throw new NotImplementedException();
        public Line3D JEnd => throw new NotImplementedException();

        public Line3D[] Edges => new[]
        {
            IStart, IEnd,
            JStart, JEnd
        };

        public Point3D[] Vertices => new[]
        {
            IStart.IStart, IEnd.IStart,
            JStart.IStart, JEnd.IStart
        };

        public long Perimeter => 2 * (IStart.Length + JStart.Length);
        public long Area => I.Length * J.Length;

        public override string ToString() => $"{IStart.IStart}-{IEnd.IEnd}";
    }
}