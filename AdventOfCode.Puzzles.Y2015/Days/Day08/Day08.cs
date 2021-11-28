namespace AdventOfCode.Puzzles.Y2015.Days.Day08;

public class Day08 : Day
{
    public override Output Part1()
    {
        var parsed = Input.Lines().Select(x => (Old: x, New: x.Parse<string>(@"
            ('""'/?)
            (
            - '\' [""\\]  / '.'
            - '\x' .^2 / '.'
            - .
            )*
            ('""'/?)
        "))).ToList();
        return parsed.Sum(x => x.Old.Length) - parsed.Sum(x => x.New.Length);
    }

    public override Output Part2()
    {
        var parsed = Input.Lines().Select(x => (Old: x, New: x.Parse<string>(@"
            (?/'""')
            (
            - [""\\] / '\?'
            - .
            )*
            (?/'""')
        "))).ToList();
        return parsed.Sum(x => x.New.Length) - parsed.Sum(x => x.Old.Length);
    }
}
