using Microsoft.VisualBasic.CompilerServices;
using System;
using Xunit;
using AdventOfCode.Base;
using AdventOfCode.Base.Tests;

namespace AdventOfCode.Y2015.Tests
{
    public class Day03 : DayTests
    {
        [Theory]
        [InlineData(">", "2")]
        [InlineData("^>v<", "4")]
        [InlineData("^v^v^v^v^v", "2")]
        [InlineData(null, null)]
        public override void Part1(string? input, string? output) => Test(input, output);

        [Theory]
        [InlineData("^v", "3")]
        [InlineData("^>v<", "3")]
        [InlineData("^v^v^v^v^v", "11")]
        [InlineData(null, null)]
        public override void Part2(string? input, string? output) => Test(input, output);
    }
}
