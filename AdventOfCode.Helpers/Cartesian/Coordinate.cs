using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace AdventOfCode.Helpers.Cartesian
{
    public struct Coordinate : IEnumerable<int>
    {
        public int[] Value { get; }

        public int Rank => Value.Length;

        public double Distance => Math.Sqrt(Value.Sum(x => x * x));

        public int X => Value[0];
        public int Y => Value[1];
        public int Z => Value[2];

        public int this[int index] => Value[index];

        public Coordinate(params int[] value)
        {
            Value = value;
        }

        public void Deconstruct(out int x)
        {
            x = X;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public override bool Equals(object? obj)
        {
            return obj is Coordinate coordinate &&
                Value.SequenceEqual(coordinate.Value);
        }

        public override int GetHashCode() => Value.Aggregate(0, (x, y) => HashCode.Combine(x, y));

        public override string ToString() => $"({String.Join(',', Value)})";

        public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Value.GetEnumerator();

        public static Coordinate operator +(Coordinate left, Coordinate right)
        {
            if (left.Rank != right.Rank)
                throw new InvalidOperationException("Cannot add coordinates with different ranks.");
            return new(left.Zip(right).Select(x => x.First + x.Second).ToArray());
        }

        public static Coordinate operator -(Coordinate left, Coordinate right)
        {
            if (left.Rank != right.Rank)
                throw new InvalidOperationException("Cannot add coordinates with different ranks.");
            return new(left.Zip(right).Select(x => x.First - x.Second).ToArray());
        }

        public static Coordinate operator -(Coordinate value) => new(value.Select(x => -x).ToArray());

        public static Coordinate operator *(Coordinate left, int right) => new(left.Select(x => x * right).ToArray());
        public static Coordinate operator *(int left, Coordinate right) => new(right.Select(x => left * x).ToArray());
        public static Coordinate operator /(Coordinate left, int right) => new(left.Select(x => x / right).ToArray());
        public static Coordinate operator /(int left, Coordinate right) => new(right.Select(x => left / x).ToArray());

        public static bool operator ==(Coordinate left, Coordinate right) => left.Equals(right);

        public static bool operator !=(Coordinate left, Coordinate right) => !(left == right);

        public static implicit operator Coordinate(int x) => new(x);
        public static implicit operator Coordinate((int x, int y) t) => new(t.x, t.y);
        public static implicit operator Coordinate((int x, int y, int z) t) => new(t.x, t.y, t.z);
    }
}