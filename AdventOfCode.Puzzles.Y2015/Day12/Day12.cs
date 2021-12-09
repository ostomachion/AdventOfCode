using System.Text;

namespace AdventOfCode.Puzzles.Y2015.Days.Day12;

public class Day12 : Day
{
    public override Output Part1()
    {
        var test = Input.Parse<KList<int>>(@"((\D)*?  ('-'? \d)+:Items  )*  (\D)* ");
        return test.Sum();
    }

    public override Output Part2()
    {
        return AnswerNotFound();
    }
}
