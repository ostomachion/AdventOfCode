namespace AdventOfCode.Puzzles.Y2021.Days.Day01;

public class Day01 : Day
{
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
