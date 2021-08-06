using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Helpers.Cartesian.Boxes;
using AdventOfCode.Helpers.DataStructures;

namespace AdventOfCode.Helpers.Cartesian.Grids
{
    public record SparseLineGrid2D<T> : Line2D, IEnumerable<KeyValuePair<Coordinate2D, T>>
        where T : notnull
    {
        public new static SparseLineGrid2D<T> Infinite => new(Interval.Infinite);

        private readonly DefaultDictionary<Coordinate2D, T> values = new();

        public IEnumerable<Coordinate2D> Keys => this.values.Keys;
        public IEnumerable<T> Values => this.values.Values;

        public T? this[Coordinate2D c]
        {
            get => Contains(c) ? values[Loop(c)] : throw new IndexOutOfRangeException();
            set => values[Loop(c)] = Contains(c) ? value : throw new IndexOutOfRangeException();
        }

        public T? this[long x, long y]
        {
            get => this[new Coordinate2D(x, y)];
            set => this[new Coordinate2D(x, y)] = value;
        }

        public SparseLineGrid2D(Interval i, long j = 0)
            : base(i, j) { }

        public IEnumerator<KeyValuePair<Coordinate2D, T>> GetEnumerator() => this.values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}