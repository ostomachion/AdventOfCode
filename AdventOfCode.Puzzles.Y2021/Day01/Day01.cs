namespace AdventOfCode.Puzzles.Y2021.Days.Day01;

public partial class Day01 : Day
{
    public override string? TestInput => @"199
200
208
210
200
207
240
269
260
263";

    public override Output? TestOutputPart1 => 1;
    public override Output? TestOutputPart2 => 5;

    public override Output Part1()
    {
        return Input.Lines().Select(int.Parse)
            .Windows(2).Count(x => x[0] < x[1]);
    }

    public override Output Part2()
    {
        return Input.Lines().Select(int.Parse)
            .Windows(3).Select(x => x.Sum())
            .Windows(2).Count(x => x[0] < x[1]);
    }
}
