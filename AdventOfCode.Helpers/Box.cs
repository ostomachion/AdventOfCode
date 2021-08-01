using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    public class Box
    {
        public IEnumerable<int> Dimensions { get; }
        public int Rank { get; }

        public long Volume => Dimensions.Product();

        public long SurfaceArea => Faces.Sum(x => x.Volume);

        public IEnumerable<Box> Faces
        {
            get
            {
                List<Box> value = new(2 * Rank);
                for (var i = 0; i < Rank; i++)
                {
                    var faceDimensions = Dimensions.Take(i).Concat(Dimensions.Skip(i + 1)).ToArray();
                    value.Add(new Box(faceDimensions));
                    value.Add(new Box(faceDimensions));
                }
                return value;
            }
        }

        public Box(params int[] dimensions)
        {
            Dimensions = dimensions;
            Rank = dimensions.Length;
        }

        public override string ToString() => String.Join('x', Dimensions);

    }
}