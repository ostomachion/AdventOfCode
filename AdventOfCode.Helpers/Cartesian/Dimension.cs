using System;
using AdventOfCode.Helpers.Extensions;

namespace AdventOfCode.Helpers.Cartesian
{
    public struct Dimension
    {
        public long? Start { get; set; }
        public long? End { get; set; }
        public bool Looping { get; set; }

        public long? Length => End - Start;

        public Dimension(long? start = null, long? end = null, bool looping = false)
        {
            if (looping && (start is null || end is null))
                throw new ArgumentException("Only finite dimensions can loop.", nameof(looping));

            Start = start;
            End = end;
            Looping = looping;
        }

        public bool Contains(long n) => (Start is null || n >= Start) && (End is null || n <= End);
        public long Loop(long n) => Length.HasValue ? n.Modulus(Length.Value) + Start!.Value : throw new InvalidOperationException();

        public override bool Equals(object? obj) => obj is Dimension dimension &&
            Start == dimension.Start &&
            End == dimension.End &&
            Looping == dimension.Looping;

        public override int GetHashCode() => HashCode.Combine(Start, End, Looping);

        public override string ToString()
        {
            return (Start.HasValue ? "[" + Start : "(-inf") +
                "," +
                (End.HasValue ? End + "]" : "inf)") +
                (Looping ? "..." : "");
        }

        public static bool operator ==(Dimension left, Dimension right) => left.Equals(right);

        public static bool operator !=(Dimension left, Dimension right) => !(left == right);

        public static implicit operator Dimension(long length) => new(0, length);
    }
}