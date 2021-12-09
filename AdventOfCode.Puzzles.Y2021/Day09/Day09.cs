namespace AdventOfCode.Puzzles.Y2021.Days.Day09;

public class Day09 : Day
{
    public override string? TestInput => @"2199943210
3987894921
9856789892
8767896789
9899965678";

    public override Output? TestOutputPart1 => 15;
    public override Output? TestOutputPart2 => 1134;

    public override Output Part1()
    {
        var input = Input.Lines();

        SparsePlaneGrid2D<int> grid = new(input[0].Length, input.Length);
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                grid[x, y] = Int32.Parse(input[y][x].ToString()) + 1;
            }
        }

        var points = grid.Where(x => grid.OrthogonalNeighbors(x.Key).All(y => grid[y] > x.Value));

        var answer = points.Sum(x => x.Value);

        return answer;
    }

    public override Output Part2()
    {
        var input = Input.Lines();

        SparsePlaneGrid2D<int> grid = new(input[0].Length, input.Length);
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                grid[x, y] = Int32.Parse(input[y][x].ToString()) + 1;
            }
        }

        var points = grid.Where(x => grid.OrthogonalNeighbors(x.Key).All(y => grid[y] > x.Value));

        List<Coordinate2D> ch = new();
        var sizes = points.Select(c => Neighbors(c.Key)).OrderByDescending(x => x).Take(3).Select(x => x + 1);
        return sizes.Product();

        int Neighbors(Coordinate2D c)
        {
            ch.Add(c);
            var points = grid.OrthogonalNeighbors(c).Where(x => !ch.Contains(x) && grid[x] != 10);
            return points.Sum(x => 1 + Neighbors(x));
        }
    }
}
