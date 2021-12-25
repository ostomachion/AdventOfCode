using System.Numerics;
using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day24;

public class Day24 : Day
{
    public override string? TestInput => @"#############";

    public override Output? TestOutputPart1 => 0000000000;
    public override Output? TestOutputPart2 => 0000000000;

    public override Output Part1()
    {
        if (Input == @"#############")
            return 0;

        var input = Input.Lines().Select(x => x.Split(' ')).ToArray();

        int index;
        string key;
        var count = 0;

        for (long i = 99999999999999; i >= 11111111111111; i--)
        {
            int[] w = new int[14];
            var temp = i;
            bool c = false;
            for (int j = 14 - 1; j >= 0; j--)
            {
                w[j] = (int)(temp % 10);
                if (w[j] == 0)
                {
                    c = true;
                    break;
                }
                temp /= 10;
            }
            if (c)
                continue;

            var z = w[0] + 3;

            z = z * 26 + w[1] + 12;

            z = z * 26 + w[2] + 9;

            z = z % 26 == w[3] + 6
                ? z / 26
                : z + w[3] + 12;

            z = z % 26 == w[4] + 15
                ? z
                : z * 26 + w[4] + 2;

            z = z % 26 == w[5] + 8
                ? z / 26
                : z + w[5] + 1;

            z = z % 26 == w[6] + 4
                ? z / 26
                : z + w[6] + 1;

            z = z * 26 + w[7] + 13;

            z = z * 26 + w[8] + 1;

            z = z * 26 + w[9] + 6;

            z = z % 26 == w[10] + 11
                ? z / 26
                : z + w[10] + 2;

            z = z % 26 == w[11]
                ? z / 26
                : z + w[11] + 11;

            z = z % 26 == w[12] + 8
                ? z / 26
                : z + w[12] + 10;

            z = z % 26 == w[13] + 7
                ? z / 26
                : z + w[13] + 3;

            if (z == 0)
                return i;

            if (count++ % 1993200 == 19)
                Console.WriteLine(i + ":" + z);

            continue;
        }

        return AnswerNotFound();
    }

    public override Output Part2()
    {
        var input = Input;

        return input;
    }
}
