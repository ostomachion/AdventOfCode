namespace AdventOfCode.Puzzles.Y2015.Tests;

public class Day02 : DayTests
{
    [Theory]
    [InlineData("2x3x4", "58")]
    [InlineData("1x1x10", "43")]
    [InlineData(null, null)]
    public override void Part1(string? input, string? output) => Test(input, output);

    [Theory]
    [InlineData("2x3x4", "34")]
    [InlineData("1x1x10", "14")]
    [InlineData(null, null)]
    public override void Part2(string? input, string? output) => Test(input, output);
}
