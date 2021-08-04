using System;
namespace AdventOfCode.Helpers.Cartesian
{
    public struct Orientation3D
    {
        public static readonly Orientation3D I = new(1, 0, 0, 0, 1, 0, 0, 0, 1);
        public static readonly Orientation3D Ryz = new(1, 0, 0, 0, 0, 1, 0, -1, 0);
        public static readonly Orientation3D Ryz2 = Ryz * Ryz;
        public static readonly Orientation3D Ryz3 = Ryz * Ryz * Ryz;
        public static readonly Orientation3D Rxz = new(0, 0, -1, 0, 1, 0, 1, 0, 0);
        public static readonly Orientation3D Rxz2 = Rxz * Rxz;
        public static readonly Orientation3D Rxz3 = Rxz * Rxz * Rxz;
        public static readonly Orientation3D Rxy = new(0, 1, 0, -1, 0, 0, 0, 0, 1);
        public static readonly Orientation3D Rxy2 = Rxy * Rxy;
        public static readonly Orientation3D Rxy3 = Rxy * Rxy * Rxy;

        public int XX { get; }
        public int XY { get; }
        public int XZ { get; }
        public int YX { get; }
        public int YY { get; }
        public int YZ { get; }
        public int ZX { get; }
        public int ZY { get; }
        public int ZZ { get; }

        public Point3D XAxis => new(XX, XY, XZ);
        public Point3D YAxis => new(YX, YY, YZ);
        public Point3D ZAxis => new(ZX, ZY, ZZ);

        public Orientation3D(int xx, int xy, int xz, int yx, int yy, int yz, int zx, int zy, int zz)
        {
            XX = xx;
            XY = xy;
            XZ = xz;
            YX = yx;
            YY = yy;
            YZ = yz;
            ZX = zx;
            ZY = zy;
            ZZ = zz;
        }

        public Orientation3D(Point3D xAxis, Point3D yAxis, Point3D zAxis)
        {
            XX = (int)xAxis.X;
            XY = (int)xAxis.Y;
            XZ = (int)xAxis.Z;
            YX = (int)yAxis.X;
            YY = (int)yAxis.Y;
            YZ = (int)yAxis.Z;
            ZX = (int)zAxis.X;
            ZY = (int)zAxis.Y;
            ZZ = (int)zAxis.Z;
        }

        public override bool Equals(object? obj)
        {
            return obj is Orientation3D d &&
                   XX == d.XX &&
                   XY == d.XY &&
                   XZ == d.XZ &&
                   YX == d.YX &&
                   YY == d.YY &&
                   YZ == d.YZ &&
                   ZX == d.ZX &&
                   ZY == d.ZY &&
                   ZZ == d.ZZ;
        }

        public override int GetHashCode()
        {
            HashCode hash = new();
            hash.Add(XX);
            hash.Add(XY);
            hash.Add(XZ);
            hash.Add(YX);
            hash.Add(YY);
            hash.Add(YZ);
            hash.Add(ZX);
            hash.Add(ZY);
            hash.Add(ZZ);
            return hash.ToHashCode();
        }

        public static bool operator ==(Orientation3D left, Orientation3D right) => left.Equals(right);

        public static bool operator !=(Orientation3D left, Orientation3D right) => !(left == right);

        public static Orientation3D operator *(Orientation3D left, Orientation3D right)
        {
            return new Orientation3D(
                left.XX * right.XX + left.XY * right.YX + left.XZ * right.ZX,
                left.XX * right.XY + left.XY * right.YY + left.XZ * right.ZY,
                left.XX * right.XZ + left.XY * right.YZ + left.XZ * right.ZZ,

                left.YX * right.XX + left.YY * right.YX + left.YZ * right.ZX,
                left.YX * right.XY + left.YY * right.YY + left.YZ * right.ZY,
                left.YX * right.XZ + left.YY * right.YZ + left.YZ * right.ZZ,

                left.ZX * right.XX + left.ZY * right.YX + left.ZZ * right.ZX,
                left.ZX * right.XY + left.ZY * right.YY + left.ZZ * right.ZY,
                left.ZX * right.XZ + left.ZY * right.YZ + left.ZZ * right.ZZ);
        }

        public static Point3D operator *(Orientation3D left, Point3D right)
        {
            return new(left.XAxis * right, left.YAxis * right, left.ZAxis * right);
        }


        public static Orientation3D operator -(Orientation3D value) => new(
            -value.XX, -value.XY, -value.XZ,
            -value.YX, -value.YY, -value.YZ,
            -value.ZX, -value.ZY, -value.ZZ);
    }
}