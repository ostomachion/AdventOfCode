using AdventOfCode.Helpers.Extensions;

namespace AdventOfCode.Helpers.Cartesian;

public record Interval
{
    public static readonly Interval Infinite = new();

    public long? OptionalStart { get; }
    public long? OptionalEnd { get; }
    public bool Looping { get; }

    public bool HasStart => OptionalStart.HasValue;
    public bool HasEnd => OptionalEnd.HasValue;
    public long Start => OptionalStart!.Value;
    public long End => OptionalEnd!.Value;

    public bool IsInfinite => !HasStart || !HasEnd;

    public long Length => End - Start;

    public Interval(long? start = null, long? end = null, bool looping = false)
    {
        if (looping && (start is null || end is null))
        {
            throw new ArgumentException("Only finite dimensions can loop.", nameof(looping));
        }

        if (end < start)
        {
            throw new ArgumentException("End cannot be less than start.", nameof(end));
        }

        OptionalStart = start;
        OptionalEnd = end;
        Looping = looping;
    }

    public bool Contains(long n) => (!HasStart || n >= Start) && (!HasEnd || n <= End);
    public long Loop(long n) => n.Modulus(Length) + Start;

    public override string ToString()
    {
        return (HasStart ? "[" + Start : "(-inf") +
            "," +
            (HasEnd ? End + "]" : "inf)") +
            (Looping ? "%" : "");
    }

    public static Interval operator -(Interval value) => new(-value.OptionalEnd, -value.OptionalStart, value.Looping);

    public static implicit operator Interval(long length) => new(0, length);
}
