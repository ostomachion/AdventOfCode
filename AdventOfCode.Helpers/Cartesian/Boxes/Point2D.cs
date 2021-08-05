using System;

namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Point2D(long I, long J)
    {
        public Orientation2D Orientation { get; init; } = Orientation2D.Standard;
        public long X => (Orientation.Inverse() * new Vector2D(I, J)).X;
        public long Y => (Orientation.Inverse() * new Vector2D(I, J)).Y;

        public Vector2D Vector => this;

        public override string? ToString() => $"({X},{Y})";

        public static implicit operator Point2D(Vector2D value) => new(
            value.X,
            value.Y);

        public static implicit operator Vector2D(Point2D value) => new(
            value.X,
            value.Y);
    }
}