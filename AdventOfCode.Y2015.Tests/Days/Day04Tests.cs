using Microsoft.VisualBasic.CompilerServices;
using System;
using Xunit;
using AdventOfCode.Base;
using AdventOfCode.Base.Tests;

namespace AdventOfCode.Y2015.Tests
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
