using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventOfCode.Helpers.Cartesian.Boxes;

namespace AdventOfCode.Helpers.Cartesian
{
    public record Vector1D(long X)
    {
        public static readonly Vector1D O = new(0);
        public static readonly Vector1D I = new(1);
        public static readonly Vector1D In = new(-1);

        public static readonly Vector1D LeftToRight = I;
        public static readonly Vector1D RightToLeft = -I;

        public static Vector1D Parse(string iChars, string inChars, char c) =>
            iChars.Contains(c) ? I :
            inChars.Contains(c) ? -I :
            O;

        public static Vector1D ParseArrows(char c) => Parse(">", "<", c);
        public static IEnumerable<Vector1D> ParseArrows(string s) => s.Select(ParseArrows);
        public static Vector1D ParseEW(char c) => Parse("Ee", "Ww", c);
        public static IEnumerable<Vector1D> ParseEW(string s) => s.Select(ParseEW);
        public static Vector1D ParseRL(char c) => Parse("Rr", "Ll", c);
        public static IEnumerable<Vector1D> ParseRL(string s) => s.Select(ParseRL);

        public static Vector1D operator +(Vector1D left, Vector1D right) => new(
            left.X + right.X);

        public static Vector1D operator -(Vector1D left, Vector1D right) => new(
            left.X - right.X);

        public static Vector1D operator -(Vector1D value) => new(
            -value.X);

        public static Vector1D operator *(Vector1D left, long right) => new(
            left.X * right);

        public static long operator *(Vector1D left, Vector1D right) =>
            left.X * right.X;

        public static implicit operator Vector1D(Coordinate1D value) => new(
            value.X);

        public static implicit operator Coordinate1D(Vector1D value) => new(
            value.X);
    }
}