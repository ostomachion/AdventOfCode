using System.Numerics;
namespace AdventOfCode.Puzzles;

public class Output
{
    public string Value { get; }

    public Output(string value)
    {
        Value = value;
    }

    public static implicit operator string(Output value) => value.Value;
    public static implicit operator Output(string value) => new(value);
    public static implicit operator Output(char value) => new(value.ToString());
    public static implicit operator Output(int value) => new(value.ToString());
    public static implicit operator Output(uint value) => new(value.ToString());
    public static implicit operator Output(BigInteger value) => new(value.ToString());
    public static implicit operator Output(short value) => new(value.ToString());
    public static implicit operator Output(ushort value) => new(value.ToString());
    public static implicit operator Output(long value) => new(value.ToString());
    public static implicit operator Output(ulong value) => new(value.ToString());
    public static implicit operator Output(float value) => new(value.ToString());
    public static implicit operator Output(double value) => new(value.ToString());
    public static implicit operator Output(decimal value) => new(value.ToString());
    public static implicit operator Output(bool value) => new(value.ToString());


    public override bool Equals(object? obj) => obj is Output output && Value == output.Value;

    public override int GetHashCode() => HashCode.Combine(Value);

    public override string ToString() => Value.ToString();
}
