using System;
using System.Numerics;
namespace AdventOfCode.Helpers.Numeric;

public struct Natural
{
    public BigInteger Value { get; }

    public Natural(BigInteger value) => Value = BigInteger.Max(0, value);

    public override bool Equals(object? obj) => obj is Natural natural && Value.Equals(natural.Value);

    public override int GetHashCode() => HashCode.Combine(Value);

    public static implicit operator Natural(BigInteger value) => new(value);
    public static implicit operator BigInteger(Natural value) => value.Value;
    public static implicit operator Natural(long value) => new(value);

    public static Natural operator +(Natural left, Natural right) => new(left.Value + right.Value);
    public static Natural operator -(Natural left, Natural right) => new(left.Value - right.Value);
    public static Natural operator *(Natural left, Natural right) => new(left.Value * right.Value);
    public static Natural operator /(Natural left, Natural right) => new(left.Value / right.Value);
    public static Natural operator %(Natural left, Natural right) => new(left.Value % right.Value);

    public static bool operator ==(Natural left, Natural right) => left.Equals(right);

    public static bool operator !=(Natural left, Natural right) => !(left == right);
}
