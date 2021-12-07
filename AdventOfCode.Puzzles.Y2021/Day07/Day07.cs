namespace AdventOfCode.Puzzles.Y2021.Days.Day07;

public class Day07 : Day
{
    public override Output Part1()
    {
        var input = Input.Split(',').Select(Int32.Parse);
        return Enumerable.Range(0, input.Max()).Select(x => input.Select(y => Math.Abs(x - y)).Sum()).Min();
    }

    public override Output Part2()
    {
        var input = Input.Split(',').Select(Int32.Parse);
        return Enumerable.Range(0, input.Max()).Select(x => input.Select(y => Math.Abs(x - y).Triangle()).Sum()).Min();
    }
}
