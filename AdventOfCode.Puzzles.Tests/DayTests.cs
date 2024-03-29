﻿using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Tests;

public abstract class DayTests
{
    private static readonly Runner runner = new();

    public abstract void Part1(string? input, string? output);
    public abstract void Part2(string? input, string? output);

    protected static void Test(string? input, string? output, [CallerFilePath] string callerFilePath = null!, [CallerMemberName] string callerMemberName = null!)
    {
        var match = Regex.Match(callerFilePath, $@"\\AdventOfCode\.Puzzles\.Y(?<year>\d\d\d\d)\.Tests\\Days\\Day(?<day>\d\d)Tests\.cs$");

        if (!match.Success)
        {
            throw new InvalidOperationException();
        }

        int year = int.Parse(match.Groups["year"].Value);
        int day = int.Parse(match.Groups["day"].Value);
        int part = callerMemberName switch
        {
            "Part1" => 1,
            "Part2" => 2,
            _ => throw new InvalidOperationException()
        };

        output ??= GetOutput(year, day, part);

        var actual = runner.Run(year, day, part, input);
        Assert.Equal(output, actual);
    }

    private static string GetOutput(int year, int day, int part)
    {
        return File.ReadAllText(Paths.GetOutputPath(year, day, part)).TrimEnd('\r', '\n');
    }
}
