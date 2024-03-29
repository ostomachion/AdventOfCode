﻿using System.Numerics;
using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day21;

public class Day21 : Day
{
    public override string? TestInput => @"0000000000";

    public override Output? TestOutputPart1 => 739785;
    public override Output? TestOutputPart2 => 444356092776315;

    public override Output Part1()
    {
        SparseLineGrid1D<int> board = new SparseLineGrid1D<int>(new Interval(1, 11, true));

        int p1Score = 0;
        int p2Score = 0;

        int p1 = 10 - 1;
        int p2 = 1 - 1;
        if (Input == @"0000000000")
        {
            p1 = 4 - 1;
            p2 = 8 - 1;
        }

        int d = 0;
        int dc = 0;
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                d++;
                dc++;
                d %= 100;
                p1 += d;
                p1 %= 10;
            }
            p1Score += p1 + 1;
            if (p1Score >= 1000)
            {
                return p2Score * dc;
            }
            for (int i = 0; i < 3; i++)
            {
                d++;
                dc++;
                d %= 100;
                p2 += d;
                p2 %= 10;
            }
            p2Score += p2 + 1;
            if (p2Score >= 1000)
            {
                return p1Score * dc;
            }
        }
    }

    public override Output Part2()
    {
        // 3 444 555555 6666666 777777 888 9
        long p1Wins;
        long p2Wins;
        if (Input == @"0000000000")
        {
            (p1Wins, p2Wins) = Wins(4 - 1, 8 - 1, 0, 0);
        }
        else
        {
            (p1Wins, p2Wins) = Wins(10 - 1, 1 - 1, 0, 0);
        }

        return Math.Max(p1Wins, p2Wins);

    }

    private static readonly Dictionary<(int, int, long, long), (long, long)> memo = new();
    private static (long, long) Wins(int p1, int p2, long p1Score, long p2Score)
    {
        if (p1Score >= 21)
            return (1, 0);
        if (p2Score >= 21)
            return (0, 1);

        if (!memo.ContainsKey((p1, p2, p1Score, p2Score)))
        {
            var (p1V, p2V) = (0L, 0L);
            for (int r1 = 1; r1 <= 3; r1++)
            {
                for (int r2 = 1; r2 <= 3; r2++)
                {
                    for (int r3 = 1; r3 <= 3; r3++)
                    {
                        var newP1 = (p1 + r1 + r2 + r3) % 10;
                        var newP1Score = p1Score + newP1 + 1;

                        var (p2S, p1S) = Wins(p2, newP1, p2Score, newP1Score);
                        p1V += p1S;
                        p2V += p2S;
                    }
                }
            }
            memo[(p1, p2, p1Score, p2Score)] = (p1V, p2V);
        }

        return memo[(p1, p2, p1Score, p2Score)];
    }
}
