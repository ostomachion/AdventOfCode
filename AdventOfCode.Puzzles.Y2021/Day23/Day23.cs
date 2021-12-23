using System.Numerics;
using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day23;

public record Amphipod(char Char, long Moves);

public class Day23 : Day
{
    public override string? TestInput => @"#############
#...........#
###B#C#B#D###
  #A#D#C#A#
  #########";

    public override Output? TestOutputPart1 => 12521;
    public override Output? TestOutputPart2 => 0000000000;

    public override Output Part1()
    {
        SparsePlaneGrid2D<bool> wallGrid = new();
        SparsePlaneGrid2D<Amphipod> amphipodGrid = new();
        var lines = Input.Lines();
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                var c = lines[y][x];
                if (char.IsLetter(c))
                    amphipodGrid[x, y] = new(c, 0);
                else
                    wallGrid[x, y] = c == '#';
            }
        }

        return Steps(wallGrid, amphipodGrid, Int64.MaxValue).Min();
    }

    private static readonly IEnumerable<Coordinate2D> hallwayStops = new Coordinate2D[]
    {
            new(1, 1), new(2, 1), new(4, 1), new(6, 1), new(8, 1), new(10, 1), new(11, 1)
    };
    private static readonly IEnumerable<Coordinate2D> roomStops = new Coordinate2D[]
    {
            new(3, 2), new(5, 2), new(7, 2), new(9, 2),
            new(3, 3), new(5, 3), new(7, 3), new(9, 3)
    };

    private static readonly HashSet<string> seen = new();
    private static IEnumerable<long> Steps(SparsePlaneGrid2D<bool> wallGrid, SparsePlaneGrid2D<Amphipod> amphipodGrid, long min)
    {
        var key = string.Join("", amphipodGrid.OrderBy(x => x.Value.Char).ThenBy(x => x.Key.X).ThenBy(x => x.Key.Y).Select(x => x.Key.ToString()));
        if (seen.Contains(key))
            yield break;
        seen.Add(key);

        foreach (var (coordinate, amphipod) in amphipodGrid.OrderBy(x => x.Key.Y).ToArray())
        {
            IEnumerable<Coordinate2D> stops;
            if (coordinate.Y == 1)
            {
                stops = roomStops;
            }
            else if (amphipod.Moves == 0)
            {
                stops = hallwayStops;
            }
            else
            {
                stops = Enumerable.Empty<Coordinate2D>();
            }

            //var paths = stops.Select(x => GetPath(coordinate, x)).Where(x => x.Any() && x.All(x => amphipodGrid[x] is null));
            var paths = new Dictionary<Coordinate2D, Coordinate2D[]>();
            foreach (var stop in stops)
            {
                if (stop.Y == 2 && (amphipodGrid[stop.X, stop.Y + 1] is not Amphipod a || amphipod.Char != stop.X switch { 3 => 'A', 5 => 'B', 7 => 'C', 9 => 'D', _ => throw new InvalidOperationException() }))
                    continue;

                var path = GetPath(coordinate, stop);
                if (path.Any(x => wallGrid[x]))
                    throw new InvalidOperationException();

                if (path.Any() && path.All(x => amphipodGrid[x] is null))
                    paths.Add(stop, path);
            }
            foreach (var (stop, path) in paths.OrderBy(x => x.Value.Length))
            {
                amphipodGrid[coordinate] = null;
                amphipodGrid[stop] = amphipod with { Moves = amphipod.Moves + path.Length };

                var value = amphipodGrid.Sum(x => x.Value.Moves * (x.Value.Char switch
                {
                    'A' => 1,
                    'B' => 10,
                    'C' => 100,
                    'D' => 1000,
                    _ => throw new InvalidOperationException()
                }));

                if (value < min)
                {
                    var success =
                        amphipodGrid[new Coordinate2D(3, 2)]?.Char == 'A' &&
                        amphipodGrid[new Coordinate2D(3, 3)]?.Char == 'A' &&
                        amphipodGrid[new Coordinate2D(5, 2)]?.Char == 'B' &&
                        amphipodGrid[new Coordinate2D(5, 3)]?.Char == 'B' &&
                        amphipodGrid[new Coordinate2D(7, 2)]?.Char == 'C' &&
                        amphipodGrid[new Coordinate2D(7, 3)]?.Char == 'C' &&
                        amphipodGrid[new Coordinate2D(9, 2)]?.Char == 'D' &&
                        amphipodGrid[new Coordinate2D(9, 3)]?.Char == 'D';

                    if (success)
                    {
                        min = value;
                        Console.WriteLine(value);
                        //Console.ReadKey(true);
                        yield return value;
                    }
                    else
                    {
                        foreach (var result in Steps(wallGrid, amphipodGrid, min))
                        {
                            if (result < min)
                            {
                                min = result;
                                yield return result;
                            }
                        }
                    }
                }

                amphipodGrid[coordinate] = amphipod;
                amphipodGrid[stop] = null;
            }
        }

    }

    private static Dictionary<(Coordinate2D, Coordinate2D), Coordinate2D[]> memo = new();
    private static Coordinate2D[] GetPath(Coordinate2D start, Coordinate2D end)
    {
        if (!memo.ContainsKey((start, end)))
        {
            memo.Add((start, end), GetPathLocal(start, end).ToArray());
        }

        return memo[(start, end)];

        static IEnumerable<Coordinate2D> GetPathLocal(Coordinate2D start, Coordinate2D end)
        {
            var current = start;
            if (start.Y == 1)
            {
                while (current != end)
                {
                    if (current.X != end.X)
                    {
                        current = current with { X = current.X + (current.X < end.X ? 1 : -1) };
                    }
                    else
                    {
                        current = current with { Y = current.Y + (current.Y < end.Y ? 1 : -1) };
                    }
                    yield return current;
                }
            }
            else
            {
                while (current != end)
                {
                    if (current.Y != end.Y)
                    {
                        current = current with { Y = current.Y + (current.Y < end.Y ? 1 : -1) };
                    }
                    else
                    {
                        current = current with { X = current.X + (current.X < end.X ? 1 : -1) };
                    }
                    yield return current;
                }
            }
        }
    }

    private static void DrawGrid(SparsePlaneGrid2D<bool> wallGrid, SparsePlaneGrid2D<Amphipod> amphipodGrid, long min, Coordinate2D? highlight = null, IEnumerable<Coordinate2D>? path = null)
    {
        return;
        Console.Clear();
        for (var y = 0; y < 5; y++)
        {
            for (var x = 0; x < 13; x++)
            {
                if (highlight == new Coordinate2D(x, y))
                    Console.ForegroundColor = ConsoleColor.Cyan;
                if (path is not null && path.Contains(new Coordinate2D(x, y)))
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                if (amphipodGrid[x, y] is Amphipod a)
                {
                    Console.Write(a.Char);
                }
                else
                {
                    Console.Write(wallGrid[x, y] ? '#' : ' ');
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        Console.WriteLine(min);
        Console.ReadKey(true);
    }

    public override Output Part2()
    {
        return AnswerNotFound();
    }
}
