using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day13;

record Inst(string Axis, int Coord);

public class Day13 : Day
{
    public override string? TestInput => @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

    public override Output? TestOutputPart1 => 17;
    public override Output? TestOutputPart2 => null;

    public override Output Part1()
    {
        var points = Input.Paragraphs()[0].Lines().Parse<Coordinate2D>(@"(\d+):X ',' (\d+):Y").ToArray();
        var folds = Input.Paragraphs()[1].Lines().Parse<Inst>(@"'fold along ' \w:Axis '=' (\d+):Coord").ToArray();

        SparsePlaneGrid2D<int> grid = new();
        foreach (var point in points)
        {
            grid[point] = 1;
        }

        var n = grid.Values.Count();

        foreach (var f in folds)
        {
            var ps = grid.Keys.ToArray();
            for (int i = 0; i < ps.Length; i++)
            {
                if (f.Axis == "x" && ps[i].X < f.Coord)
                {
                    var c = new Coordinate2D(f.Coord - (ps[i].X - f.Coord), ps[i].Y);
                    grid[c] += grid[ps[i]];
                    grid[ps[i]] = 0;
                }
                else if (f.Axis == "y" && ps[i].Y < f.Coord)
                {
                    var c = new Coordinate2D(ps[i].X, f.Coord - (ps[i].Y - f.Coord));
                    grid[c] += grid[ps[i]];
                    grid[ps[i]] = 0;
                }
            }
            if (grid.Values.Sum() != n)
                throw new Exception();
            break;
        }

        var answer = grid.Values.Count();

        return answer;
    }

    public override Output Part2()
    {
        var points = Input.Paragraphs()[0].Lines().Parse<Coordinate2D>(@"(\d+):X ',' (\d+):Y").ToArray();
        var folds = Input.Paragraphs()[1].Lines().Parse<Inst>(@"'fold along ' \w:Axis '=' (\d+):Coord").ToArray();

        SparsePlaneGrid2D<int> grid = new();
        foreach (var point in points)
        {
            grid[point] = 1;
        }

        var n = grid.Values.Count();

        foreach (var f in folds)
        {
            var ps = grid.Keys.ToArray();
            for (int i = 0; i < ps.Length; i++)
            {
                if (f.Axis == "x" && ps[i].X > f.Coord)
                {
                    var c = new Coordinate2D(f.Coord - (ps[i].X - f.Coord), ps[i].Y);
                    grid[c] += grid[ps[i]];
                    grid[ps[i]] = 0;
                }
                else if (f.Axis == "y" && ps[i].Y > f.Coord)
                {
                    var c = new Coordinate2D(ps[i].X, f.Coord - (ps[i].Y - f.Coord));
                    grid[c] += grid[ps[i]];
                    grid[ps[i]] = 0;
                }
            }
            if (grid.Values.Sum() != n)
                throw new Exception();

        }

        var result = new char[grid.Keys.Max(x => (int)x.Y + 1)][];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new char[grid.Keys.Max(x => (int)x.X + 1)];
        }
        foreach (var c in grid.Keys)
        {
            result[(int)c.Y][(int)c.X] = '#';
        }
        StringBuilder x = new();
        foreach (var i in result)
        {
            foreach (var j in i)
            {
                x.Append(j == '\0' ? ' ' : j);
            }
            x.Append('\n');
        }
        var a = x.ToString();

        return x.ToString();
    }
}
