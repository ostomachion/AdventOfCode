namespace AdventOfCode.Puzzles.Y2015.Tests;

public class Day08 : DayTests
{
    [Theory]
    [InlineData(@"""""
""abc""
""aaa\""aaa""
""\x27""", "12")]
    [InlineData(null, null)]
    public override void Part1(string? input, string? output) => Test(input, output);

    [Theory]
    [InlineData(@"""""
""abc""
""aaa\""aaa""
""\x27""", "19")]
    [InlineData(null, null)]
    public override void Part2(string? input, string? output) => Test(input, output);
}
