using Microsoft.VisualBasic.CompilerServices;
using System;
using Xunit;
using AdventOfCode.Puzzles;
using AdventOfCode.Puzzles.Tests;

namespace AdventOfCode.Puzzles.Y2015.Tests
{
    public class Day04 : DayTests
    {
        [Theory]
        [InlineData("abcdef", "609043")]
        [InlineData("pqrstuv", "1048970")]
        [InlineData(null, null)]
        public override void Part1(string? input, string? output) => Test(input, output);

        [Theory]
        [InlineData(null, null)]
        public override void Part2(string? input, string? output) => Test(input, output);
    }
}
