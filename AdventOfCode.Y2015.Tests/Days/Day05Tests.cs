using Microsoft.VisualBasic.CompilerServices;
using System;
using Xunit;
using AdventOfCode.Base;
using AdventOfCode.Base.Tests;

namespace AdventOfCode.Y2015.Tests
{
    public class Day05 : DayTests
    {
        [Theory]
        [InlineData("ugknbfddgicrmopn", "1")]
        [InlineData("aaa", "1")]
        [InlineData("jchzalrnumimnmhp", "0")]
        [InlineData("haegwjzuvuyypxyu", "0")]
        [InlineData("dvszwmarrgswjxmb", "0")]
        [InlineData(null, null)]
        public override void Part1(string? input, string? output) => Test(input, output);

        [Theory]
        [InlineData("qjhvhtzxzqqjkmpb", "1")]
        [InlineData("xxyxx", "1")]
        [InlineData("uurcxstgmygtbstg", "0")]
        [InlineData("ieodomkazucvgmuy", "0")]
        [InlineData(null, null)]
        public override void Part2(string? input, string? output) => Test(input, output);
    }
}
