namespace AdventOfCode.Puzzles.Y2021.Days.Day12;

record Node(string A, string B);

public class Day12 : Day
{
    public override string? TestInput => @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc";

    public override Output? TestOutputPart1 => 10;
    public override Output? TestOutputPart2 => 103;

    public override Output Part1()
    {
        var lines = Input.Lines().Select(x => x.Split('-')).ToArray();
        var input = lines.Select(x => new Node(x[0], x[1])).ToList();

        var reversed = input.Select(x => new Node(x.B, x.A)).ToList();
        input.AddRange(reversed);

        Stack<string> stack = new();
        stack.Push("start");
        return Count("end");
        
        int Count(string end)
        {
            var value = 0;
            foreach (var line in input!.Where(x => x.A == stack.Peek()))
            {
                if (char.IsLower(line.B[0]) && stack.Count(x => x == line.B) == 2)
                    continue;

                if (line.B == end)
                    value++;

                stack.Push(line.B);
                value += Count(end);
                stack.Pop();
            }

            return value;
        }
    }

    public override Output Part2()
    {
        var lines = Input.Lines().Select(x => x.Split('-')).ToArray();
        var input = lines.Select(x => new Node(x[0], x[1])).ToList();


        var smallCaves = input.SelectMany(x => new[] { x.A, x.B }).Where(x => x != "start" && x != "end" && char.IsLower(x[0])).ToList();

        var reversed = input.Select(x => new Node(x.B, x.A)).ToList();
        input.AddRange(reversed);

        Stack<string> stack = new();
        stack.Push("start");
        return Count("end");

        int Count(string end)
        {
            var value = 0;
            foreach (var line in input!.Where(x => x.A == stack.Peek()))
            {
                if (char.IsLower(line.B[0]) && stack.Contains(line.B) && (line.B == "start" || line.B == "end" || smallCaves.Any(x => stack.Count(y => y == x) == 2)))
                    continue;

                if (line.B == end)
                    value++;

                stack.Push(line.B);
                value += Count(end);
                stack.Pop();
            }

            return value;
        }
    }
}
