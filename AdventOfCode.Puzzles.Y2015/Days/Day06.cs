using Kleene;

namespace AdventOfCode.Puzzles.Y2015.Days;

public class Day06 : Day
{
    public record MatchValue(int Toggle, Coordinate2D Start, Coordinate2D End);
    public override Output Part1()
    {
        var input = Input.Lines().Parse<MatchValue>(@"
            <coord> { \d+:X ',' \d+:Y }
            (('turn on'/1) | ('toggle'/0) | ('turn off'/-1)):Toggle
            ' ' <coord>:Start ' through ' <coord>:End
        ");

        var grid = new SparsePlaneGrid2D<bool>(1000, 1000);
        foreach (var item in input)
        {
            for (var x = item!.Start.X; x <= item.End.X; x++)
            {
                for (var y = item.Start.Y; y <= item.End.Y; y++)
                {
                    switch (item.Toggle)
                    {
                        case 1:
                            grid[x, y] = true;
                            break;
                        case 0:
                            grid[x, y] = !grid[x, y];
                            break;
                        case -1:
                            grid[x, y] = false;
                            break;
                    }
                }
            }
        }
        return grid.Values.Count(x => x);
    }

    public override Output Part2()
    {
        var input = Input.Lines().Parse<MatchValue>(@"
            <coord> { \d+:X ',' \d+:Y }
            (('turn on'/1) | ('toggle'/0) | ('turn off'/-1)):Toggle
            ' ' <coord>:Start ' through ' <coord>:End
        ");

        var grid = new SparsePlaneGrid2D<Natural>(1000, 1000);
        foreach (var item in input)
        {
            for (var x = item!.Start.X; x <= item.End.X; x++)
            {
                for (var y = item.Start.Y; y <= item.End.Y; y++)
                {
                    switch (item.Toggle)
                    {
                        case 1:
                            grid[x, y] = grid[x, y] + 1;
                            break;
                        case 0:
                            grid[x, y] = grid[x, y] + 2;
                            break;
                        case -1:
                            grid[x, y] = grid[x, y] - 1;
                            break;
                    }
                }
            }
        }
        return grid.Values.Sum(x => (long)x.Value);
    }
}
