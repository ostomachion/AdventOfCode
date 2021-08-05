using System;

namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Space3D(Interval I, Interval J, Interval K)
    {
        public Orientation3D Orientation { get; init; } = Orientation3D.Standard;

        public Plane3D IStart => new(J, K, I.Start) { Orientation =  new Orientation3D(Vector3D.BottomToTop, Vector3D.FarToNear, Vector3D.LeftToRight) * Orientation };
        public Plane3D IEnd => new(-J, -K, -I.End) { Orientation =  new Orientation3D(Vector3D.TopToBottom, Vector3D.NearToFar, Vector3D.RightToLeft) * Orientation };
        public Plane3D JStart => new(K, I, J.Start) { Orientation =  new Orientation3D(Vector3D.FarToNear, Vector3D.LeftToRight, Vector3D.BottomToTop) * Orientation };
        public Plane3D JEnd => new(-K, -I, -J.Start) { Orientation =  new Orientation3D(Vector3D.NearToFar, Vector3D.RightToLeft, Vector3D.TopToBottom) * Orientation };
        public Plane3D KStart => new(I, J, K.Start) { Orientation = Orientation3D.Standard * Orientation };
        public Plane3D KEnd =>  new(-I, -J, -K.Start) { Orientation = -Orientation3D.Standard * Orientation };

        public Plane3D[] Faces => new []
        {
            KStart, JStart, IStart,
            KEnd, JEnd, IEnd
        };

        public Line3D[] Edges => new []
        {
            KStart.IStart, IStart.IEnd,
            JStart.IStart, JStart.IEnd,
            IStart.IStart, IStart.IEnd,
            KEnd.IStart, KEnd.IEnd,
            JEnd.IStart, JEnd.IEnd,
            IEnd.IStart, IEnd.IEnd
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