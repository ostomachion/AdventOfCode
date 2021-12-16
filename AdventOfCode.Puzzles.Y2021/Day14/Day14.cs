using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day14;

public record Pair(char L1, char L2, char R);

public class Day14 : Day
{
    public override string? TestInput => @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

    public override Output? TestOutputPart1 => 1588;
    public override Output? TestOutputPart2 => 2188189693529L;

    public override Output Part1()
    {
        var paras = Input.Paragraphs();
        var input = paras[0].Lines()[0].ToList();
        var pairs = paras[1].Lines().Parse<Pair>(@"\w:L1 \w:L2 ' -> ' \w:R");

        for (var n = 0; n < 10; n++)
        {
            for (int i = 0; i < input.Count - 1; i++)
            {
                var l1 = input[i];
                var l2 = input[i + 1];
                var r = pairs.First(x => x.L1 == l1 && x.L2 == l2).R;
                input.Insert(i + 1, r);
                i++;
            }
        }

        var counts = input.Distinct().ToDictionary(x => x, x => input.Count(y => y == x));

        return counts.Max(x => x.Value) - counts.Min(x => x.Value);
    }

    public override Output Part2()
    {
        var paras = Input.Paragraphs();
        var input = paras[0].Lines()[0].ToList();
        var pairs = paras[1].Lines().Parse<Pair>(@"\w:L1 \w:L2 ' -> ' \w:R");

        DefaultDictionary<string, long> counts = new();
        for (var i = 0; i < input.Count - 1; i++)
        {
            counts[input[i].ToString() + input[i + 1].ToString()]++;
        }
        
        for (var n = 0; n < 40; n++)
        {
            DefaultDictionary<string, long> newCounts = new ();
            foreach (var c in counts.Keys)
            {
                var l1 = c[0];
                var l2 = c[1];
                var r = pairs.First(x => x.L1 == l1 && x.L2 == l2).R;

                newCounts[l1.ToString() + r.ToString()] += counts[c];
                newCounts[r.ToString() + l2.ToString()] += counts[c];
            }
            counts = newCounts;
        }

        var cs = new DefaultDictionary<char, long>();
        foreach (var count in counts.Keys)
        {
            cs[count[0]] += counts[count];
            //cs[count[1]] += counts[count];
        }

        cs[input.Last()]++;

        return cs.Max(x => x.Value) - cs.Min(x => x.Value);
    }
}
