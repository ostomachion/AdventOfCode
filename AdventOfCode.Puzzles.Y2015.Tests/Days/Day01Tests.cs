using Microsoft.VisualBasic.CompilerServices;
using System;
using Xunit;
using AdventOfCode.Puzzles;
using AdventOfCode.Puzzles.Tests;

namespace AdventOfCode.Puzzles.Y2015.Tests
{
    public class Day01 : DayTests
    {
        [Theory]
        [InlineData("(())", "0")]
        [InlineData("()()", "0")]
        [InlineData("(((", "3")]
        [InlineData("(()(()(", "3")]
        [InlineData("))(((((", "3")]
        [InlineData("())", "-1")]
        [InlineData("))(", "-1")]
        [InlineData(")))", "-3")]
        [InlineData(")())())", "-3")]
        [InlineData(null, null)]
        public override void Part1(string? input, string? output) => Test(input, output);

        [Theory]
        [InlineData(")", "1")]
        [InlineData("()())", "5")]
        [InlineData(null, null)]
        public override void Part2(string? input, string? output) => Test(input, output);
    }
}
