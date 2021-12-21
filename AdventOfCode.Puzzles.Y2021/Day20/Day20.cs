using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day20;

public class Day20 : Day
{
    public override string? TestInput => @"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..##
#..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###
.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#.
.#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#.....
.#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#..
...####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.....
..##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###";

    public override Output? TestOutputPart1 => 35;
    public override Output? TestOutputPart2 => 3351;

    public override Output Part1()
    {
        var input = Input.Paragraphs();
        var algorithm = input[0].Replace("\n", "");

        var imageData = input[1].Lines();
        var grid = new SparsePlaneGrid2D<bool>();
        for (int y = 0; y < imageData.Length; y++)
        {
            for (int x = 0; x < imageData[y].Length; x++)
            {
                grid[x, y] = imageData[y][x] == '#';
            }
        }

        var x0 = 0;
        var xN = imageData[0].Length;
        var y0 = 0;
        var yN = imageData.Length;

        for (int t = 0; t < 2; t++)
        {
            x0--;
            xN++;
            y0--;
            yN++;
            var newDefault = (grid.DefaultValue ? algorithm.Last() : algorithm.First()) == '#';
            var newGrid = new SparsePlaneGrid2D<bool>(newDefault);

            for (var y = y0; y <= yN; y++)
            {
                for (var x = x0; x <= xN; x++)
                {
                    var rule = 0;
                    rule = rule * 2 + (grid[x - 1, y - 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x, y - 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x + 1, y - 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x - 1, y] ? 1 : 0);
                    rule = rule * 2 + (grid[x, y] ? 1 : 0);
                    rule = rule * 2 + (grid[x + 1, y] ? 1 : 0);
                    rule = rule * 2 + (grid[x - 1, y + 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x, y + 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x + 1, y + 1] ? 1 : 0);

                    newGrid[x, y] = algorithm[rule] == '#';
                }
            }

            grid = newGrid;
        }

        return grid.Count(x => x.Value);
    }

    public override Output Part2()
    {
        var input = Input.Paragraphs();
        var algorithm = input[0].Replace("\n", "");

        var imageData = input[1].Lines();
        var grid = new SparsePlaneGrid2D<bool>();
        for (int y = 0; y < imageData.Length; y++)
        {
            for (int x = 0; x < imageData[y].Length; x++)
            {
                grid[x, y] = imageData[y][x] == '#';
            }
        }

        var x0 = 0;
        var xN = imageData[0].Length;
        var y0 = 0;
        var yN = imageData.Length;

        for (int t = 0; t < 50; t++)
        {
            x0--;
            xN++;
            y0--;
            yN++;
            var newDefault = (grid.DefaultValue ? algorithm.Last() : algorithm.First()) == '#';
            var newGrid = new SparsePlaneGrid2D<bool>(newDefault);

            for (var y = y0; y <= yN; y++)
            {
                for (var x = x0; x <= xN; x++)
                {
                    var rule = 0;
                    rule = rule * 2 + (grid[x - 1, y - 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x, y - 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x + 1, y - 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x - 1, y] ? 1 : 0);
                    rule = rule * 2 + (grid[x, y] ? 1 : 0);
                    rule = rule * 2 + (grid[x + 1, y] ? 1 : 0);
                    rule = rule * 2 + (grid[x - 1, y + 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x, y + 1] ? 1 : 0);
                    rule = rule * 2 + (grid[x + 1, y + 1] ? 1 : 0);

                    newGrid[x, y] = algorithm[rule] == '#';
                }
            }

            grid = newGrid;
        }

        return grid.Count(x => x.Value);
    }
}
