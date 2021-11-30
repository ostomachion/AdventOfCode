namespace AdventOfCode.Puzzles.Y2015.Days.Day09;

public record Item(string Start, string End, long Distance)
{
    public static long GetDistance(IEnumerable<Item> items, IEnumerable<string> p)
    {
        var path = p.ToArray();
        if (path.Length == 2)
        {
            return items.First(x => x.Start == path[0] && x.End == path[1] || x.Start == path[1] && x.End == path[0]).Distance;
        }   
        else
        {
            return GetDistance(items, path[0..2]) + GetDistance(items, path[2..]);
        }
    }
}