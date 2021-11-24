using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode.Puzzles.Tests;

public abstract class DayTests
{
    private static readonly Runner runner = new();

    public abstract void Part1(string? input, string? output);
    public abstract void Part2(string? input, string? output);

    protected static async void Test(string? input, string? output, [CallerFilePath] string callerFilePath = null!, [CallerMemberName] string callerMemberName = null!)
    {
        // C:\Users\josh\Source\Repos\AdventOfCode\AdventOfCode.Puzzles.Y2015.Tests\Days\Day05Tests.cs
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

        var actual = await DayTests.runner.RunAsync(year, day, part, input);
        Assert.Equal(output, actual);
    }

    private static string GetOutput(int year, int day, int part)
    {
        return File.ReadAllText(Paths.GetOutputPath(year, day, part)).TrimEnd('\r', '\n');
    }
}
