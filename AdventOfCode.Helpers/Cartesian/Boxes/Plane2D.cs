using System;

namespace AdventOfCode.Helpers.Cartesian.Boxes
{
    public record Plane2D(Interval I, Interval J)
    {
        public static readonly Plane2D Infinite = new(Interval.Infinite, Interval.Infinite);

        public Orientation2D Orientation { get; init; } = Orientation2D.Standard;

        public Line2D IStart => new(-J, I.Start) { Orientation = new Orientation2D(Vector2D.TopToBottom, Vector2D.LeftToRight) * Orientation };
        public Line2D IEnd => new(J, -I.End) { Orientation = new Orientation2D(Vector2D.BottomToTop, Vector2D.RightToLeft) * Orientation };
        public Line2D JStart => new(I, J.Start) { Orientation = Orientation2D.Standard * Orientation };
        public Line2D JEnd => new(-I, -J.End) { Orientation = new Orientation2D(Vector2D.RightToLeft, Vector2D.TopToBottom) * Orientation };

        public Line2D[] Edges => new[]
        {
            JStart, IEnd,
            JEnd, IStart
        };

        public Point2D[] Vertices => new[]
        {
            JStart.IStart, IEnd.IStart,
            JEnd.IStart, IStart.IStart
        };

        public long Perimeter => 2 * (IStart.Length + IEnd.Length);
        public long? Area => I.Length * J.Length;

        public bool Contains(Coordinate2D coordinate)
        {
            coordinate = Loop(coordinate);
            return I.Contains(coordinate.X) && J.Contains(coordinate.Y);
        }

        public Coordinate2D Loop(Coordinate2D coordinate) => coordinate with
        {
            X = I.Looping ? I.Loop(coordinate.X) : coordinate.X,
            Y = J.Looping ? J.Loop(coordinate.Y) : coordinate.Y,
        };

        public override string ToString() => $"{IStart.IStart}-{IEnd.IEnd}";
    }
}