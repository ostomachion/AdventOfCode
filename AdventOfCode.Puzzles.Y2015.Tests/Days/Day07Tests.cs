using AdventOfCode.Puzzles.Y2015.Days;

namespace AdventOfCode.Puzzles.Y2015.Tests;

public class Day07 : DayTests
{
    [Theory]
    [InlineData(@"123 -> x
456 -> y
x AND y -> a", "72")]
    [InlineData(@"123 -> x
456 -> y
x OR y -> a", "507")]
    [InlineData(@"123 -> x
456 -> y
x LSHIFT 2 -> a", "492")]
    [InlineData(@"123 -> x
456 -> y
y RSHIFT 2 -> a", "114")]
    [InlineData(@"123 -> x
456 -> y
NOT x -> a", "65412")]
    [InlineData(@"123 -> x
456 -> y
NOT y -> a", "65079")]
    [InlineData(null, null)]
    public override void Part1(string? input, string? output) => Test(input, output);

    [Theory]
    [InlineData(null, null)]
    public override void Part2(string? input, string? output) => Test(input, output);
}
