namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public static class Box
    {
        public static Line1D Make(Interval x) => new(x);
        public static Plane2D Make(Interval x, Interval y) => new(x, y);
        public static Space3D Make(Interval x, Interval y, Interval z) => new(x, y, z);
    }
}