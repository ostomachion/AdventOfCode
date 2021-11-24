namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Point3D(long I, long J, long K)
    {
        public Orientation3D Orientation { get; init; } = Orientation3D.Standard;
        public long X => (Orientation.Inverse() * new Vector3D(I, J, K)).X;
        public long Y => (Orientation.Inverse() * new Vector3D(I, J, K)).Y;
        public long Z => (Orientation.Inverse() * new Vector3D(I, J, K)).Z;

        public Vector3D Vector => this;

        public void Deconstruct(out long x, out long y, out long z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public override string? ToString() => $"({X},{Y},{Z})";

        public static implicit operator Point3D(Coordinate3D value) => new(
            value.X,
            value.Y,
            value.Z);

        public static implicit operator Coordinate3D(Point3D value) => new(
            value.X,
            value.Y,
            value.Z);

        public static implicit operator Point3D(Vector3D value) => new(
            value.X,
            value.Y,
            value.Z);

        public static implicit operator Vector3D(Point3D value) => new(
            value.X,
            value.Y,
            value.Z);
    }
}