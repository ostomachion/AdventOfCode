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
        return val;
    }

    public override Output Part2()
    {
        var test = Input.Lines().Select(Int32.Parse).ToArray();
        int val = 0;
        var prev = 9999999;
        for (int i = 0; i < test.Length - 2; i++)
        {
            int x = test[i] + test[i + 1] + test[i + 2];
            if (x > prev)
                val++;
            prev = x;
        }
        return val;
    }
}
