using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace AdventOfCode.Helpers.Cartesian
{
    public class Grid<T> : Box
    {
        public BoxArrayItem<T> Values { get; }

        public T? this[params int[] indexes] { get => Values[indexes]; set => Values[indexes] = value; }
        public T? this[Coordinate c] { get => Values[c]; set => Values[c] = value; }

        public Grid(params int[] dimensions)
            : base(dimensions)
        {
            Values = dimensions.Any() ? new BoxArray<T>(dimensions) : new BoxArrayCell<T>(default);
        }
    }

    public abstract class BoxArrayItem<T> : IEnumerable<T?>
    {
        public abstract T? this[params int[] indexes] { get; set; }
        public T? this[Coordinate c]
        {
            get => this[c.Value.ToArray()];
            set => this[c.Value.ToArray()] = value;
        }

        public abstract int Rank { get; }
        public abstract IEnumerable<int> Dimensions { get; }

        public abstract IEnumerator<T?> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class BoxArrayCell<T> : BoxArrayItem<T>
    {
        public T? Value { get; set; }
        public override int Rank => 0;
        public override IEnumerable<int> Dimensions => Enumerable.Empty<int>();

        public override T? this[params int[] indexes]
        {
            get
            {
                if (indexes.Any())
                    throw new ArgumentException("Indexes must match rank", nameof(indexes));
                else
                    return Value;
            }
            set
            {
                if (indexes.Any())
                    throw new ArgumentException("Indexes must match rank", nameof(indexes));
                else
                    this.Value = value;
            }
        }

        public BoxArrayCell(T? value)
        {
            Value = value;
        }

        public override IEnumerator<T?> GetEnumerator()
        {
            yield return Value;
        }
    }

    public class BoxArray<T> : BoxArrayItem<T>
    {
        public BoxArrayItem<T>[] Values { get; }
        public override IEnumerable<int> Dimensions { get; }
        public override int Rank { get; }

        public override T? this[params int[] indexes]
        {
            get => Values[indexes.First()][indexes.Skip(1).ToArray()];
            set => Values[indexes.First()][indexes.Skip(1).ToArray()] = value;
        }

        public BoxArray(params int[] dimensions)
        {
            Dimensions = dimensions;
            Rank = dimensions.Length;
            if (Rank == 0)
            {
                throw new ArgumentException("BoxArray must have at least one dimension.", nameof(dimensions));
            }
            else if (Rank == 1)
            {
                Values = new BoxArrayCell<T>[dimensions[0]];
                for (var i = 0; i < Values.Length; i++)
                {
                    Values[i] = new BoxArrayCell<T>(default);
                }
            }
            else
            {
                Values = new BoxArray<T>[dimensions.First()];
                var childDimensions = dimensions.Skip(1).ToArray();
                for (var i = 0; i < Values.Length; i++)
                {
                    Values[i] = new BoxArray<T>(childDimensions);
                }
            }
        }

        public override IEnumerator<T?> GetEnumerator()
        {
            foreach (var row in Values)
            {
                foreach (var item in row)
                {
                    yield return item;
                }
            }
        }
    }
}