using AdventOfCode.Helpers.Cartesian;
using AdventOfCode.Helpers.Cartesian.Grids;

namespace AdventOfCode.Puzzles.Y2015.Days;

public class Day03 : Day
{
    public override Output Part1()
    {
        var path = Vector2D.ParseArrows(Input);
        var grid = Grid.Infinite2D<int>();
        var c = Coordinate2D.O;
        grid[c]++;
        foreach (var p in path)
        {
            c += p;
            grid[c]++;
        }
        return grid.Values.Count();
    }

    public override Output Part2()
    {
        var path = Vector2D.ParseArrows(Input);
        var grid = Grid.Infinite2D<int>();
        var s = Coordinate2D.O;
        var r = Coordinate2D.O;
        grid[s]++;
        grid[r]++;
        bool isS = true;
        foreach (var p in path)
        {
            if (isS)
            {
                s += p;
                grid[s]++;
            }
            else
            {
                r += p;
                grid[r]++;
            }
            isS = !isS;
        }
        return grid.Values.Count();
    }
}
