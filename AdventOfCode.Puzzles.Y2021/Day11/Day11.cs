namespace AdventOfCode.Puzzles.Y2021.Days.Day11;

public class Day11 : Day
{
    public override string? TestInput => @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

    public override Output? TestOutputPart1 => 1656;
    public override Output? TestOutputPart2 => 195;

    public override Output Part1()
    {
        var input = Input.Lines();
        var grid = new SparsePlaneGrid2D<int>(input[0].Length, input.Length);
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                grid[x, y] = Int32.Parse(input[y][x].ToString()) + 1;
            }
        }

        List<Coordinate2D> flashes = new();
        long answer = 0;
        for (int i = 0; i < 100; i++)
        {
            flashes.Clear();
            for (int y = 0; y < grid.J.Length; y++)
            {
                for (int x = 0; x < grid.I.Length; x++)
                {
                    Increase(x, y);
                }
            }
        }

        void Increase(long x, long y)
        {
            if (flashes.Contains(new(x, y)))
                return;

            grid[x, y]++;
            if (grid[x, y] == 11)
            {
                answer++;
                grid[x, y] = 1;
                if (!flashes.Contains(new(x, y)))
                {
                    flashes.Add(new(x, y));
                    var ns = grid.Neighbors(new(x, y));
                    foreach (var n in ns)
                    {
                        Increase(n.X, n.Y);
                    }
                }
            }
        }

        return answer;
    }

    private void Print(SparsePlaneGrid2D<int> grid)
    {
        Console.Clear();
        for (int y = 0; y < grid.J.Length; y++)
        {
            for (int x = 0; x < grid.I.Length; x++)
            {
                var v = grid[x, y] - 1;
                if (v == 0)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(v);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    public override Output Part2()
    {
        var input = Input.Lines();
        var grid = new SparsePlaneGrid2D<int>(input[0].Length, input.Length);
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                grid[x, y] = Int32.Parse(input[y][x].ToString()) + 1;
            }
        }

        List<Coordinate2D> flashes = new();
        long answer = 0;
        var i = 0;
        while (true)
        {
            flashes.Clear();
            for (int y = 0; y < grid.J.Length; y++)
            {
                for (int x = 0; x < grid.I.Length; x++)
                {
                    Increase(x, y);
                }
            }
            if (flashes.Count == grid.Count())
                return i + 1;
            i++;
        }

        void Increase(long x, long y)
        {
            if (flashes.Contains(new(x, y)))
                return;

            grid[x, y]++;
            if (grid[x, y] == 11)
            {
                answer++;
                grid[x, y] = 1;
                if (!flashes.Contains(new(x, y)))
                {
                    flashes.Add(new(x, y));
                    var ns = grid.Neighbors(new(x, y));
                    foreach (var n in ns)
                    {
                        Increase(n.X, n.Y);
                    }
                }
            }
        }
    }
}
