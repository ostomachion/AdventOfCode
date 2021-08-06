using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Helpers.Cartesian.Boxes;
using AdventOfCode.Helpers.DataStructures;

namespace AdventOfCode.Helpers.Cartesian.Grids
{
    public record SparseLineGrid1D<T> : Line1D, IEnumerable<KeyValuePair<Coordinate1D, T>>
        where T : notnull
    {
        public new static SparseLineGrid1D<T> Infinite => new(Interval.Infinite);

        private readonly DefaultDictionary<Coordinate1D, T> values = new();

        public IEnumerable<Coordinate1D> Keys => this.values.Keys;
        public IEnumerable<T> Values => this.values.Values;

        public T? this[Coordinate1D c]
        {
            get => Contains(c) ? values[Loop(c)] : throw new IndexOutOfRangeException();
            set => values[Loop(c)] = Contains(c) ? value : throw new IndexOutOfRangeException();
        }

        public T? this[long x]
        {
            get => this[new Coordinate1D(x)];
            set => this[new Coordinate1D(x)] = value;
        }

        public SparseLineGrid1D(Interval i)
            : base(i) { }

        public IEnumerator<KeyValuePair<Coordinate1D, T>> GetEnumerator() => this.values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}