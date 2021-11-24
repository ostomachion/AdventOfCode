using System;
namespace AdventOfCode.Helpers.Extensions;

public static class MathExtenions
{
    public static long Modulus(this long left, long right) => ((left % right) + right) % right;

    public static bool IsBetween(this long n, long min, long max) => n >= min && n <= max;

    public static long Clamp(this long n, long min, long max) => Math.Min(Math.Max(n, min), max);
}
