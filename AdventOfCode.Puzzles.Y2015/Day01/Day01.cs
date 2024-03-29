namespace AdventOfCode.Puzzles.Y2015.Days.Day01;

public class Day01 : Day
{
    public override Output Part1()
    {
        return Input.Count('(') - Input.Count(')');
    }

    public override Output Part2()
    {
        int l = 0;
        foreach (var (i, c) in Input.ToDictionary())
        {
            l += c == '(' ? 1 : -1;
            if (l < 0)
            {
                return i + 1;
            }
        }

        return AnswerNotFound();
    }
}
