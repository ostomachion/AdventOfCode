namespace AdventOfCode.Puzzles.Y2021.Days.Day05;

public class Day05 : Day
{
    record Test(int X1, int X2, int Y1, int Y2);
    public override Output Part1()
    {
        var test = Input.Lines().Parse<Test>(@"\d+:X1 ',' \d+:Y1 ' -> ' \d+:X2 ',' \d+:Y2").ToArray();

        var grid = SparsePlaneGrid2D<int>.Infinite;
        Console.Clear();
        foreach (var line in test)
        {
            if (line.X1 == line.X2 || line.Y1 == line.Y2)
            {
                for (var x = Math.Min(line.X1, line.X2); x <= Math.Max(line.X1, line.X2); x++)
                {
                    for (var y = Math.Min(line.Y1, line.Y2); y <= Math.Max(line.Y1, line.Y2); y++)
                    {
                        grid[x, y]++;
                    }
                }
            }
        }
        return grid.Count(x => x.Value > 1);
    }

    public override Output Part2()
    {
        var test = Input.Lines().Parse<Test>(@"\d+:X1 ',' \d+:Y1 ' -> ' \d+:X2 ',' \d+:Y2").ToArray();

        var grid = SparsePlaneGrid2D<int>.Infinite;
        Console.Clear();
        foreach (var line in test)
        {
            if (line.X1 == line.X2 || line.Y1 == line.Y2)
            {
                for (var x = Math.Min(line.X1, line.X2); x <= Math.Max(line.X1, line.X2); x++)
                {
                    for (var y = Math.Min(line.Y1, line.Y2); y <= Math.Max(line.Y1, line.Y2); y++)
                    {
                        grid[x, y]++;
                    }
                }
            }
            else if (Math.Abs(line.X1 - line.X2) == Math.Abs(line.Y1 - line.Y2))
            {
                var x = line.X1;
                var y = line.Y1;
                for (int i = 0; i <= Math.Abs(line.X1 - line.X2); i++)
                {
                    grid[x, y]++;
                    x += x < line.X2 ? 1 : -1;
                    y += y < line.Y2 ? 1 : -1;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        return grid.Count(x => x.Value > 1);
    }
}
