namespace AdventOfCode.Puzzles.Y2015.Days.Day09;

public class Day09 : Day
{
    public override Output Part1()
    {
        var parsed = Input.Lines().Parse<Item>(@"\w+:Start ' to ' \w+:End ' = ' \d+:Distance");
        HashSet<string> locations = parsed.SelectMany(x => new[] { x.Start, x.End }).ToHashSet();
        
        return AnswerNotFound();
    }

    public override Output Part2()
    {
        throw new NotImplementedException();
    }
}
