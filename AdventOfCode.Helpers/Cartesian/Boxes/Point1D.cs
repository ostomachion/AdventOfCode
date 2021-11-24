namespace AdventOfCode.Helpers.Cartesian.Boxes;

public record Point1D(long I)
{
    public Orientation1D Orientation { get; init; } = Orientation1D.Standard;
    public long X => (Orientation.Inverse() * new Vector1D(I)).X;

    public Vector1D Vector => this;

    public void Deconstruct(out long x)
    {
        x = X;
    }

    public override string? ToString() => $"({X})";

    public static implicit operator Point1D(Coordinate1D value) => new(
        value.X);

    public static implicit operator Coordinate1D(Point1D value) => new(
        value.X);

    public static implicit operator Point1D(Vector1D value) => new(
        value.X);

    public static implicit operator Vector1D(Point1D value) => new(
        value.X);
}
