using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using AdventOfCode.Base;
using Xunit;

namespace AdventOfCode.Base.Tests
{
    public abstract class DayTests
    {
        private static readonly Runner runner = new();

        public abstract void Part1(string? input, string? output);
        public abstract void Part2(string? input, string? output);

        protected static void Test(string? input, string? output)
        {
            var caller = new StackFrame(1).GetMethod()!;
            var match = Regex.Match(caller.ReflectedType!.FullName!, $@"^AdventOfCode\.Y(?<year>\d\d\d\d)\.Tests\.Day(?<day>\d\d)$");
            
            if (!match.Success)
                throw new InvalidOperationException();

            int year = Int32.Parse(match.Groups["year"].Value);
            int day = Int32.Parse(match.Groups["day"].Value);
            int part = caller.Name switch
            {
                "Part1" => 1,
                "Part2" => 2,
                _ => throw new InvalidOperationException()
            };

            output ??= GetOutput(year, day, part);

            var actual = DayTests.runner.Run(year, day, part, input);
            Assert.Equal(output, actual);
        }

        private static string GetOutput(int year, int day, int part)
        {
            return File.ReadAllText(Paths.GetOutputPath(year, day, part)).TrimEnd('\n');
        }
    }
}