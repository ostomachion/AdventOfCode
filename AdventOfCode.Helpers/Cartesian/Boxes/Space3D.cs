using System;

namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Space3D(Interval I, Interval J, Interval K)
    {
        public Orientation3D Orientation { get; init; } = Orientation3D.Standard;

        public Plane3D IStart => throw new NotImplementedException();
        public Plane3D IEnd => throw new NotImplementedException();
        public Plane3D JStart => throw new NotImplementedException();
        public Plane3D JEnd => throw new NotImplementedException();
        public Plane3D KStart => throw new NotImplementedException();
        public Plane3D KEnd => throw new NotImplementedException();

        public Plane3D[] Faces => new []
        {
            IStart, IEnd,
            JStart, JEnd,
            KStart, KEnd
        };

        public Line3D[] Edges => new []
        {
            IStart.IStart, IStart.IEnd,
            IEnd.IStart, IEnd.IEnd,
            JStart.IStart, JStart.IEnd,
            JEnd.IStart, JEnd.IEnd,
            KStart.IStart, KStart.IEnd,
            KEnd.IStart, KEnd.IEnd,
        };
        
        public Point3D[] Vertices => new []
        {
            IStart.IStart.IStart, IStart.IStart.IEnd,
            IStart.IEnd.IStart, IStart.IEnd.IEnd,
            IEnd.IStart.IStart, IEnd.IStart.IEnd,
            IEnd.IEnd.IStart, IEnd.IEnd.IEnd
        };

        public long SurfaceArea => 2 * (IStart.Area + JStart.Area + KStart.Area);
        public long Volume => I.Length * J.Length * K.Length;

        public override string ToString() => $"{IStart.IStart.IStart}-{IEnd.IEnd.IEnd}";
    }
}