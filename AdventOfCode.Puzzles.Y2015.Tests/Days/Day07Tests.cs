using AdventOfCode.Puzzles.Y2015.Days;

namespace AdventOfCode.Puzzles.Y2015.Tests;

public class Day07 : DayTests
{
    const string input = @"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i";

    [Theory]
    [InlineData(input, "72", "d")]
    [InlineData(input, "507", "e")]
    [InlineData(input, "492", "f")]
    [InlineData(input, "114", "g")]
    [InlineData(input, "65412", "h")]
    [InlineData(input, "65079", "i")]
    [InlineData(input, "123", "x")]
    [InlineData(input, "456", "y")]
    [InlineData(null, null, "a")]
    public void Part1Full(string? input, string? output, string dest)
    {
        Days.Day07.Dest = dest;
        Part1(input, output);
    }

    public override void Part1(string? input, string? output) => Test(input, output);

    [Theory]
    [InlineData(null, null)]
    public override void Part2(string? input, string? output) => Test(input, output);
}
