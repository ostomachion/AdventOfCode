namespace AdventOfCode.Helpers.Cartesian;

public record Orientation3D
{
    public static readonly Orientation3D Standard = new(Vector3D.LeftToRight, Vector3D.BottomToTop, Vector3D.FarToNear);

    public static IEnumerable<Orientation3D> Orientations = new Orientation3D[]
    {
        new(Vector3D.LeftToRight, Vector3D.BottomToTop, Vector3D.FarToNear),
        new(Vector3D.RightToLeft, Vector3D.TopToBottom, Vector3D.FarToNear),
        new(Vector3D.RightToLeft, Vector3D.BottomToTop, Vector3D.NearToFar),
        new(Vector3D.LeftToRight, Vector3D.TopToBottom, Vector3D.NearToFar),

        new(Vector3D.LeftToRight, Vector3D.FarToNear, Vector3D.BottomToTop),
        new(Vector3D.RightToLeft, Vector3D.NearToFar, Vector3D.BottomToTop),
        new(Vector3D.RightToLeft, Vector3D.FarToNear, Vector3D.TopToBottom),
        new(Vector3D.LeftToRight, Vector3D.NearToFar, Vector3D.TopToBottom),


        new(Vector3D.BottomToTop, Vector3D.RightToLeft, Vector3D.FarToNear),
        new(Vector3D.TopToBottom, Vector3D.LeftToRight, Vector3D.FarToNear),
        new(Vector3D.TopToBottom, Vector3D.RightToLeft, Vector3D.NearToFar),
        new(Vector3D.BottomToTop, Vector3D.LeftToRight, Vector3D.NearToFar),

        new(Vector3D.BottomToTop, Vector3D.FarToNear, Vector3D.LeftToRight),
        new(Vector3D.TopToBottom, Vector3D.NearToFar, Vector3D.LeftToRight),
        new(Vector3D.TopToBottom, Vector3D.FarToNear, Vector3D.RightToLeft),
        new(Vector3D.BottomToTop, Vector3D.NearToFar, Vector3D.RightToLeft),


        new(Vector3D.FarToNear, Vector3D.BottomToTop, Vector3D.RightToLeft),
        new(Vector3D.NearToFar, Vector3D.TopToBottom, Vector3D.RightToLeft),
        new(Vector3D.NearToFar, Vector3D.BottomToTop, Vector3D.LeftToRight),
        new(Vector3D.FarToNear, Vector3D.TopToBottom, Vector3D.LeftToRight),

        new(Vector3D.FarToNear, Vector3D.LeftToRight, Vector3D.BottomToTop),
        new(Vector3D.NearToFar, Vector3D.RightToLeft, Vector3D.BottomToTop),
        new(Vector3D.NearToFar, Vector3D.LeftToRight, Vector3D.TopToBottom),
        new(Vector3D.FarToNear, Vector3D.RightToLeft, Vector3D.TopToBottom),
    };

    private readonly int xx;
    private readonly int xy;
    private readonly int xz;
    private readonly int yx;
    private readonly int yy;
    private readonly int yz;
    private readonly int zx;
    private readonly int zy;
    private readonly int zz;

    public Vector3D XAxis => new(xx, xy, xz);
    public Vector3D YAxis => new(yx, yy, yz);
    public Vector3D ZAxis => new(zx, zy, zz);

    public int Determinant =>
        xx * yy * zz +
        xy * yz * zx +
        xz * yx * zy -
        xz * yy * zx -
        xy * yx * zz -
        xx * yz * zy;

    private Orientation3D(int xx, int xy, int xz, int yx, int yy, int yz, int zx, int zy, int zz)
    {
        this.xx = xx;
        this.xy = xy;
        this.xz = xz;
        this.yx = yx;
        this.yy = yy;
        this.yz = yz;
        this.zx = zx;
        this.zy = zy;
        this.zz = zz;
    }

    public Orientation3D(Vector3D xAxis, Vector3D yAxis, Vector3D zAxis)
        : this(
            (int)xAxis.X, (int)xAxis.Y, (int)xAxis.Z,
            (int)yAxis.X, (int)yAxis.Y, (int)yAxis.Z,
            (int)zAxis.X, (int)zAxis.Y, (int)zAxis.Z)
    {
        if (xAxis * yAxis != 0 || yAxis * zAxis != 0 || zAxis * xAxis != 0 || Determinant is not (1 or -1))
        {
            throw new ArgumentException("Must be an orthonormal basis.");
        }
    }

    public static Orientation3D operator *(Orientation3D left, Orientation3D right) => new(
        left.xx * right.xx + left.xy * right.yx + left.xz * right.zx,
        left.xx * right.xy + left.xy * right.yy + left.xz * right.zy,
        left.xx * right.xz + left.xy * right.yz + left.xz * right.zz,

        left.yx * right.xx + left.yy * right.yx + left.yz * right.zx,
        left.yx * right.xy + left.yy * right.yy + left.yz * right.zy,
        left.yx * right.xz + left.yy * right.yz + left.yz * right.zz,

        left.zx * right.xx + left.zy * right.yx + left.zz * right.zx,
        left.zx * right.xy + left.zy * right.yy + left.zz * right.zy,
        left.zx * right.xz + left.zy * right.yz + left.zz * right.zz);

    public Orientation3D Inverse()
    {
        var value = new Orientation3D(
            yy * zz - yz * zy, xz * zy - xy * zz, xy * yz - xz * yy,
            yz * zx - yx * zz, xx * zz - xz * zx, xz * yx - xx * yz,
            yx * zy - yy * zx, xy * zx - xx * zy, xx * yy - xy * yx
        );
        return Determinant == 1 ? value : -value;
    }

    public static Vector3D operator *(Orientation3D left, Vector3D right) => new(
        left.XAxis * right,
        left.YAxis * right,
        left.ZAxis * right);

    public static Orientation3D operator -(Orientation3D value) => new(
        -value.xx, -value.xy, -value.xz,
        -value.yx, -value.yy, -value.yz,
        -value.zx, -value.zy, -value.zz);
}
