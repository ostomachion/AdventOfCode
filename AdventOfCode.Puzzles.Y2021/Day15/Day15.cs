using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day15;

public class Day15 : Day
{
    public override string? TestInput => @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";

    public override Output? TestOutputPart1 => 40;
    public override Output? TestOutputPart2 => 315;

    public override Output Part1()
    {
        var input = Input.Lines();
        Helpers.Cartesian.Grids.SparsePlaneGrid2D<int> grid = new(input[0].Length, input.Length);
        grid.Metric = (c) =>
        {
            return grid.OrthogonalNeighbors(c).Where(x => grid[x] != 0).Select(x => new KeyValuePair<Coordinate2D, long>(x, grid[x]));
        };

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                grid[y, x] = Int32.Parse(input[y][x].ToString());
            }
        }

        var test = grid.Distance(new(0, 0), new(input[0].Length - 1, input.Length - 1));
        return test;
    }

    public override Output Part2()
    {
        var input = Input.Lines();
        Helpers.Cartesian.Grids.SparsePlaneGrid2D<int> grid = new(input[0].Length * 5, input.Length * 5);
        Helpers.Cartesian.Grids.SparsePlaneGrid2D<long> ds = new(input[0].Length * 5, input.Length * 5);
        grid.Metric = (c) =>
        {
            return grid.OrthogonalNeighbors(c)
                .Where(grid.Contains).Select(x => new KeyValuePair<Coordinate2D, long>(x, grid[x]));
            //return grid.OrthogonalNeighbors(c).Where(x => grid[x] != 0).Select(x => new KeyValuePair<Coordinate2D, long>(x, grid[x]));
        };

        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    for (int x = 0; x < input[y].Length; x++)
                    {
                        var n = Int32.Parse(input[y][x].ToString());
                        var m = (n + i + j - 1) % 9 + 1;
                        if (input.Length * j + y == 0 && input[y].Length * i + x == 2) ;
                        grid[input.Length * j + y, input[y].Length * i + x] = m;
                        ds[input.Length * j + y, input[y].Length * i + x] = 0;
                    }
                }
            }
        }

        DefaultDictionary<Coordinate2D, long> visited = new();
        long Search(Coordinate2D c)
        {
            if (!grid.Contains(c))
                return long.MaxValue;
            if (visited.ContainsKey(c))
                return visited[c];

            visited.Add(c, -1L);

            var value = Math.Min(Search(c + Coordinate2D.I),
                Math.Min(Search(c + Coordinate2D.J),
                Math.Min(Search(c - Coordinate2D.J),
                Search(c - Coordinate2D.J))));
            visited[c] = value + grid[c];
            return visited[c];
        }

        for (int y = (int)grid.J.Length - 1; y >= 0; y--)
        {
            for (int x = (int)grid.I.Length - 1; x >= 0; x--)
            {
                int d = grid[x, y];
                long min;
                if (x == (int)grid.J.Length - 1 && y == (int)grid.I.Length - 1)
                    min = 0;
                else if (x == (int)grid.J.Length - 1)
                    min = ds[x, y + 1];
                else if (y == (int)grid.I.Length - 1)
                    min = ds[x + 1, y];
                else
                    min = Math.Min(ds[x + 1, y], ds[x, y + 1]);
                ds[x, y] = d + min;
            }
        }

        var test = grid.Distance(new(0, 0), new(input[0].Length * 5 - 1, input.Length * 5 - 1));
        return test;
    }
}
