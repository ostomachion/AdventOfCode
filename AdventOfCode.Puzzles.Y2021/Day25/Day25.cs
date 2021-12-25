using System.Numerics;
using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day25;

public class Day25 : Day
{
    public override string? TestInput => @"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>";

    public override Output? TestOutputPart1 => 58;
    public override Output? TestOutputPart2 => 0000000000;

    public override Output Part1()
    {
        var input = Input.Lines();

        var grid = new SparsePlaneGrid2D<char>(new Interval(0, input[0].Length, true), new Interval(0, input.Length, true));

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                grid[x, y] = input[y][x];
            }
        }

        var t = 0;
        while (true)
        {
            t++;
            Console.WriteLine(t);
            int moved = 0;
            var newGrid = new SparsePlaneGrid2D<char>(new Interval(0, input[0].Length, true), new Interval(0, input.Length, true));
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    newGrid[x, y] = grid[x, y];
                }
            }

            for (int y = input.Length - 1; y >= 0; y--)
            {
                for (int x = input[0].Length - 1; x >= 0; x--)
                {
                    if (grid[x, y] == '>' && grid[x + 1, y] == '.')
                    {
                        newGrid[x, y] = '.';
                        newGrid[x + 1, y] = '>';
                        moved++;
                    }
                }
            }
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    grid[x, y] = newGrid[x, y];
                }
            }
            for (int y = input.Length - 1; y >= 0; y--)
            {
                for (int x = input[0].Length - 1; x >= 0; x--)
                {
                    if (grid[x, y] == 'v' && grid[x, y + 1] == '.')
                    {
                        newGrid[x, y] = '.';
                        newGrid[x, y + 1] = 'v';
                        moved++;
                    }
                }
            }
            if (moved == 0)
                return t;

            grid = newGrid;
        }
    }

    private void Draw(SparsePlaneGrid2D<char> grid)
    {
        Console.Clear();
        for (int y = 0; y < grid.J.Length; y++)
        {
            for (int x = 0; x < grid.I.Length; x++)
            {
                Console.Write(grid[x, y]);
            }
            Console.WriteLine();
        }
    }

    public override Output Part2()
    {
        var input = Input;

        return input;
    }
}
