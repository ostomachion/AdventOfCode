namespace AdventOfCode.Puzzles.Y2021.Days.Day02;

public class Day02 : Day
{
    public override Output Part1()
    {
        var value = Input.Lines().Parse<(string Dir, int N)>(@"\w+:Dir ' ' \d+:N");

        int x = 0;
        int y = 0;
        foreach (var (dir, n) in value)
        {
            switch (dir)
            {
                case "forward":
                    x += n;
                    break;
                case "down":
                    y += n;
                    break;
                case "up":
                    y -= n;
                    break;
            }
        }
        return x * y;
    }

    public override Output Part2()
    {
        var value = Input.Lines().Parse<(string Dir, int N)>(@"\w+:Dir ' ' \d+:N");

        int x = 0;
        int y = 0;
        int a = 0;
        foreach (var (dir, n) in value)
        {
            switch (dir)
            {
                case "forward":
                    x += n;
                    y += a * n;
                    break;
                case "down":
                    a += n;
                    break;
                case "up":
                    a -= n;
                    break;
            }
        }
        return x * y;
    }
}
