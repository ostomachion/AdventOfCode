namespace AdventOfCode.Puzzles.Y2021.Days.Day02;

public record Item(string Dir, int N);

public class Day02 : Day
{
    public override Output Part1()
    {
        var value = Input.Lines().Parse<Item>(@"(forward|down|up):Dir ' ' \d+:N");

        int x = 0;
        int y = 0;
        int a = 0;
        foreach (var v in value)
        {
            switch (v.Dir)
            {
                case "forward":
                    x += v.N;
                    y += a * v.N;
                    break;
                case "down":
                    y += v.N;
                    a += v.N;
                    break;
                case "up":
                    y -= v.N;
                    a -= v.N;
                    break;
            }
        }
        return x * y;
    }

    public override Output Part2()
    {
        var value = Input.Lines().Parse<Item>(@"(forward|down|up):Dir ' ' \d+:N");

        int x = 0;
        int y = 0;
        int a = 0;
        foreach (var v in value)
        {
            switch (v.Dir)
            {
                case "forward":
                    x += v.N;
                    y += a * v.N;
                    break;
                case "down":
                    a += v.N;
                    break;
                case "up":
                    a -= v.N;
                    break;
            }
        }
        return x * y;
    }
}
