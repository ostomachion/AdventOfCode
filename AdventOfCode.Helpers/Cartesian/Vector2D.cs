using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers.Cartesian
{
    public record Vector2D(long X, long Y)
    {
        public static readonly Vector2D O = new(0, 0);
        public static readonly Vector2D I = new(1, 0);
        public static readonly Vector2D J = new(0, 1);

        public static readonly Vector2D LeftToRight = I;
        public static readonly Vector2D RightToLeft = -I;
        public static readonly Vector2D BottomToTop = J;
        public static readonly Vector2D TopToBottom = -J;

        public static Vector2D Parse(string iChars, string inChars, string jChars, string jnChars, char c) =>
            iChars.Contains(c) ? I :
            inChars.Contains(c) ? -I :
            jChars.Contains(c) ? J :
            jnChars.Contains(c) ? -J :
            O;

        public static Vector2D ParseArrows(char c) => Parse(">", "<", "^", "v", c);
        public static IEnumerable<Vector2D> ParseArrows(string s) => s.Select(ParseArrows);
        public static Vector2D ParseNSEW(char c) => Parse("Ee", "Ww", "Nn", "Ss", c);
        public static IEnumerable<Vector2D> ParseNSEW(string s) => s.Select(ParseNSEW);
        public static Vector2D ParseRLUD(char c) => Parse("Rr", "Ll", "Uu", "Dd", c);
        public static IEnumerable<Vector2D> ParseRLUD(string s) => s.Select(ParseRLUD);

        public static Vector2D operator +(Vector2D left, Vector2D right) => new(
            left.X + right.X,
            left.Y + right.Y);

        public static Vector2D operator -(Vector2D left, Vector2D right) => new(
            left.X - right.X,
            left.Y - right.Y);

        public static Vector2D operator *(Vector2D left, long right) => new(
            left.X * right,
            left.Y * right);

        public static Vector2D operator -(Vector2D value) => new(
            -value.X,
            -value.Y);

        public static long operator *(Vector2D left, Vector2D right) =>
            left.X * right.X +
            left.Y * right.Y;

        public static implicit operator Vector2D(Coordinate2D value) => new(
            value.X,
            value.Y);

        public static implicit operator Coordinate2D(Vector2D value) => new(
            value.X,
            value.Y);
    }
}