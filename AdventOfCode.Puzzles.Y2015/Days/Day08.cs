using AdventOfCode.Helpers.DataStructures;

namespace AdventOfCode.Puzzles.Y2015.Days;

public class Day08 : Day
{
    public override Output Part1()
    {
        var parsed = Input.Lines().Select(x => (Old: x, New: x.Parse<string>(@"
            ('""'/?)
            (
            - '\' [\\""] / '.' 
            - '\' \d^2 / '.'
            - .
            )*
            ('""'/?)
        "))).ToList();
        return parsed.Sum(x => x.Old.Length) - parsed.Sum(x => x.New.Length);
    }

    public override Output Part2()
    {
        throw new NotImplementedException();
    }
}
