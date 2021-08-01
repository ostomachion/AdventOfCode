using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers.Cartesian
{
    public static class Direction2D
    {
        public static readonly Coordinate None = new(0, 0);

        public static readonly Coordinate N = new(0, -1);
        public static readonly Coordinate E = new(1, 0);
        public static readonly Coordinate S = new(0, 1);
        public static readonly Coordinate W = new(-1, 0);

        public static readonly Coordinate NE = new(1, -1);
        public static readonly Coordinate NW = new(-1, -1);
        public static readonly Coordinate SE = new(1, 1);
        public static readonly Coordinate SW = new(-1, 1);

        public static Coordinate FromChar(char c) => c switch {
            '^' => Direction2D.N,
            '>' => Direction2D.E,
            'v' => Direction2D.S,
            '<' => Direction2D.W,
            _ => Direction2D.None
        };

        public static IEnumerable<Coordinate> FromString(string text) => text.Select(FromChar);
    }
}