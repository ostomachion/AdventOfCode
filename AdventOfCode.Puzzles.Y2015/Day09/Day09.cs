namespace AdventOfCode.Puzzles.Y2015.Days.Day09;

public class Day10 : Day
{
    public override Output Part1()
    {
        var parsed = Input.Lines().Parse<Item>(@"\w+:Start ' to ' \w+:End ' = ' \d+:Distance");
        HashSet<string> locations = parsed.SelectMany(x => new[] { x.Start, x.End }).ToHashSet();
        long min = long.MaxValue;

        var max = parsed.MaxBy(x => x.Distance)!;

        foreach (var p in locations.Permutations().Select(x => x.ToList()).Where(x => x.First() == max.Start && x.Last() == max.End))
        {
            long d = 0;
            for (var i = 1; i < p.Count; i++)
            {
                d += Item.GetDistance(parsed, new[] { p[i], p[i - 1] });
                if (d > min)
                    break;
            }
            if (d < min)
                min = d;
        }
        return min;
    }

    public override Output Part2()
    {
        var parsed = Input.Lines().Parse<Item>(@"\w+:Start ' to ' \w+:End ' = ' \d+:Distance").ToList();
        HashSet<string> locations = parsed.SelectMany(x => new[] { x.Start, x.End }).ToHashSet();

        long d = 0;
        while (parsed.Any())
        {
            var max = parsed.MaxBy(x => x.Distance)!;
            parsed.RemoveAll(x => x.Start == max.Start || x.End == max.Start || x.Start == max.End || x.End == max.End);
            d += max.Distance;
        }
        return d;
    }
}
