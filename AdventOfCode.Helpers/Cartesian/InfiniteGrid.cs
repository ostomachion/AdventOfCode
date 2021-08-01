using System;
using System.Diagnostics;
using System.Collections.Generic;
using AdventOfCode.Helpers.DataStructures;

namespace AdventOfCode.Helpers.Cartesian
{
    public class InfiniteGrid<T>
        where T : notnull
    {
        public DefaultDictionary<Coordinate, T> Values { get; }

        public T? this[Coordinate c]
        {
            get
            {
                if (c.Rank != Rank)
                    throw new ArgumentException($"Cannot use a rank {c.Rank} coordinate with a rank {Rank} gird.", nameof(c));
                return Values[c];
            }

            set
            {
                if (c.Rank != Rank)
                    throw new ArgumentException($"Cannot use a rank {c.Rank} coordinate with a rank {Rank} gird.", nameof(c));
                Values[c] = value;
            }
        }

        public int Rank { get; }

        public InfiniteGrid(int rank = 2, T? defaultValue = default)
        {
            Rank = rank;
            Values = new DefaultDictionary<Coordinate, T>(defaultValue);
        }
    }
}