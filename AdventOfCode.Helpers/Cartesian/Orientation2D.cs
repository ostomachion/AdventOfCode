namespace AdventOfCode.Helpers.Cartesian;

public record Orientation2D
{
    public static readonly Orientation2D Standard = new(Vector2D.LeftToRight, Vector2D.BottomToTop);

    private readonly int xx;
    private readonly int xy;
    private readonly int yx;
    private readonly int yy;

    public Vector2D XAxis => new(xx, xy);
    public Vector2D YAxis => new(yx, yy);

    public int Determinant => xx * yy - xy * yx;

    private Orientation2D(int xx, int xy, int yx, int yy)
    {
        this.xx = xx;
        this.xy = xy;
        this.yx = yx;
        this.yy = yy;
    }

    public Orientation2D(Vector2D xAxis, Vector2D yAxis)
        : this((int)xAxis.X, (int)xAxis.Y, (int)yAxis.X, (int)yAxis.Y)
    {
        if (xAxis * yAxis != 0 || Determinant is not (1 or -1))
        {
            throw new ArgumentException("Must be an orthonormal basis.");
        }
    }

    public Orientation2D Inverse()
    {
        var value = new Orientation2D(
            yy, -xy,
            -yx, xx);
        return Determinant == 1 ? value : -value;
    }

    public static Orientation2D operator *(Orientation2D left, Orientation2D right) => new(
        left.xx * right.xx + left.xy * right.yx,
        left.xx * right.xy + left.xy * right.yy,

        left.yx * right.xx + left.yy * right.yx,
        left.yx * right.xy + left.yy * right.yy);

    public static Vector2D operator *(Orientation2D left, Vector2D right) => new(
        left.XAxis * right,
        left.YAxis * right);

    public static Orientation2D operator -(Orientation2D value) => new(
        -value.xx, -value.xy,
        -value.yx, -value.yy);
}
