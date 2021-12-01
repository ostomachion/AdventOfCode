namespace AdventOfCode.Puzzles.Y2021.Days.Day01;

public class Day01 : Day
{
    public override Output Part1()
    {
        var test = Input.Lines().Select(Int32.Parse);
        int val = 0;
        var prev = 9999999;
        foreach (var line in test)
        {
            if (line > prev)
                val++;
            prev = line;
        }
        return AnswerNotFound();
    }

    public override Output Part2()
    {
        // TODO:
        throw new NotImplementedException();
    }
}
