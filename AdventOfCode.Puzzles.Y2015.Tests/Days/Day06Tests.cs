using Microsoft.VisualBasic.CompilerServices;
using System;
using Xunit;
using AdventOfCode.Puzzles;
using AdventOfCode.Puzzles.Tests;

namespace AdventOfCode.Puzzles.Y2015.Tests
{
    public class Day06 : DayTests
    {
        [Theory]
        [InlineData("turn on 0,0 through 999,999", "1000000")]
        [InlineData("toggle 0,0 through 999,0", "1000")]
        [InlineData("turn off 499,499 through 500,500", "0")]
        [InlineData(null, null)]
        public override void Part1(string? input, string? output) => Test(input, output);

        [Theory]
        [InlineData("turn on 0,0 through 0,0", "1")]
        [InlineData("toggle 0,0 through 999,999", "2000000")]
        [InlineData(null, null)]
        public override void Part2(string? input, string? output) => Test(input, output);
    }
}
