using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Helpers.Cartesian.Boxes;
using AdventOfCode.Helpers.DataStructures;

namespace AdventOfCode.Helpers.Cartesian.Grids
{
    public record SparseLineGrid3D<T> : Line3D, IEnumerable<KeyValuePair<Coordinate3D, T>>
        where T : notnull
    {
        public new static SparseLineGrid3D<T> Infinite => new(Interval.Infinite);

        private readonly DefaultDictionary<Coordinate3D, T> values = new();

        public IEnumerable<Coordinate3D> Keys => this.values.Keys;
        public IEnumerable<T> Values => this.values.Values;

        public T? this[Coordinate3D c]
        {
            get => Contains(c) ? values[Loop(c)] : throw new IndexOutOfRangeException();
            set => values[Loop(c)] = Contains(c) ? value : throw new IndexOutOfRangeException();
        }

        public T? this[long x, long y, long z]
        {
            get => this[new Coordinate3D(x, y, z)];
            set => this[new Coordinate3D(x, y, z)] = value;
        }

        public SparseLineGrid3D(Interval i, long j = 0, long k = 0)
            : base(i, j, k) { }

        public IEnumerator<KeyValuePair<Coordinate3D, T>> GetEnumerator() => this.values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}