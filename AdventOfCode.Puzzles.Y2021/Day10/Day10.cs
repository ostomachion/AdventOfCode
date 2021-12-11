namespace AdventOfCode.Puzzles.Y2021.Days.Day10;

public class Day10 : Day
{
    public override string? TestInput => @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

    public override Output? TestOutputPart1 => 26397;
    public override Output? TestOutputPart2 => 288957;

    public override Output Part1()
    {
        var input = Input.Lines();
        Dictionary<char, char> d = new Dictionary<char, char>();
        d.Add('(', ')');
        d.Add('[', ']');
        d.Add('{', '}');
        d.Add('<', '>');

        Dictionary<char, long> p = new Dictionary<char, long>();
        p.Add(')', 3);
        p.Add(']', 57);
        p.Add('}', 1197);
        p.Add('>', 25137);

        long score = 0;
        foreach (var line in input)
        {
            Stack<char> stack = new();
            foreach (var c in line)
            {
                if ("([{<".Contains(c))
                    stack.Push(c);
                else
                {
                    var pop = stack.Pop();
                    if (c != d[pop])
                    {
                        score += p[c];
                        break;
                    }
                }
            }
        }
        return score;
    }

    public override Output Part2()
    {
        var input = Input.Lines();
        Dictionary<char, char> d = new Dictionary<char, char>();
        d.Add('(', ')');
        d.Add('[', ']');
        d.Add('{', '}');
        d.Add('<', '>');

        Dictionary<char, long> p = new Dictionary<char, long>();
        p.Add(')', 1);
        p.Add(']', 2);
        p.Add('}', 3);
        p.Add('>', 4);

        List<long> scores = new();
        foreach (var line in input)
        {
            Stack<char> stack = new();
            var b = false;
            foreach (var c in line)
            {
                if ("([{<".Contains(c))
                    stack.Push(c);
                else
                {
                    var pop = stack.Pop();
                    if (c != d[pop])
                    {
                        b = true;
                        break;
                    }
                }
            }
            if (b)
                continue;
            long lineScore = 0;
            while (stack.Any())
            {
                lineScore *= 5;
                lineScore += p[d[stack.Pop()]];
            }
            scores.Add(lineScore);
        }
        return scores.OrderBy(x => x).ElementAt(scores.Count / 2);
    }
}
