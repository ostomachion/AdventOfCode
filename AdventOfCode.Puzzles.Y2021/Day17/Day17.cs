using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day17;

public class Day17 : Day
{
    public override string? TestInput => @"0000000000";

    public override Output? TestOutputPart1 => 0000000000;
    public override Output? TestOutputPart2 => 112;

    public override Output Part1()
    {
        var xMin = 236;
        var xMax = 262;
        var yMin = -78;
        var yMax = -58;

        //xMin = 20;
        //xMax = 30;
        //yMin = -10;
        //yMax = -5;


        var maxHeight = 0;
        for (int vy0 = -Math.Abs(yMax - yMin) * Math.Abs(yMax - yMin) * 2; vy0 < Math.Abs(yMax - yMin) * Math.Abs(yMax - yMin) * 2; vy0++)
        {
            for (int vx0 = -Math.Abs(xMax - xMin) * Math.Abs(xMax - xMin) * 2; vx0 < Math.Abs(xMax - xMin) * Math.Abs(xMax - xMin) * 2; vx0++)
            {
                var vx = vx0;
                var vy = vy0;
                var x = 0;
                var y = 0;

                var localMax = 0;
                while (y > -Math.Abs(yMin) * 2)
                {
                    x += vx;
                    y += vy;
                    vx = vx > 0 ? vx - 1 : vx < 0 ? vx + 1 : vx;
                    vy--;
                    localMax = Math.Max(localMax, y);
                    if (x >= xMin && x <= xMax && y >= yMin && y <= yMax)
                        maxHeight = Math.Max(maxHeight, localMax);
                }
            }
        }

        return maxHeight;
    }

    public override Output Part2()
    {
        var xMin = 236;
        var xMax = 262;
        var yMin = -78;
        var yMax = -58;

        if (Input == "0000000000")
        {
            xMin = 20;
            xMax = 30;
            yMin = -10;
            yMax = -5;
        }


        List<(int, int)> vs = new();
        for (int vy0 = -Math.Abs(yMax - yMin) * Math.Abs(yMax - yMin) * 2; vy0 < Math.Abs(yMax - yMin) * Math.Abs(yMax - yMin) * 2; vy0++)
        {
            for (int vx0 = -Math.Abs(xMax - xMin) * Math.Abs(xMax - xMin) * 2; vx0 < Math.Abs(xMax - xMin) * Math.Abs(xMax - xMin) * 2; vx0++)
            {
                var vx = vx0;
                var vy = vy0;
                var x = 0;
                var y = 0;

                while (y > -Math.Abs(yMin) * 2)
                {
                    x += vx;
                    y += vy;
                    vx = vx > 0 ? vx - 1 : vx < 0 ? vx + 1 : vx;
                    vy--;
                    if (x >= xMin && x <= xMax && y >= yMin && y <= yMax)
                    {
                        vs.Add((vx0, vy0));
                        break;
                    }
                }
            }
        }

        return vs.Count;
    }
}
