using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    public class SquareGrid
    {
        public IEnumerable<int> Dimensions { get; }
        public int Rank { get; }

        public int Volume => Dimensions.Aggregate(1, (x, y) => x * y);

        public int SurfaceArea => Faces.Nodes.Sum(x => x.Value.Volume);

        public Graph<SquareGrid> Faces
        {
            get
            {
                Graph<SquareGrid> value = new();
                for (var i = 0; i < Rank; i++)
                {
                    var faceDimensions = Dimensions.Take(i).Concat(Dimensions.Skip(i + 1)).ToArray();
                    value.Add(new SquareGrid(faceDimensions));
                    value.Add(new SquareGrid(faceDimensions));
                }
                return value;
            }
        }

        public SquareGrid(params int[] dimensions)
        {
            Dimensions = dimensions;
            Rank = dimensions.Length;
        }

        public override string ToString() => String.Join('x', Dimensions);

    }
}