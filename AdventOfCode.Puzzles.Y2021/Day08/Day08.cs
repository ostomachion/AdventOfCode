namespace AdventOfCode.Puzzles.Y2021.Days.Day08;

public class Day08 : Day
{
    public override Output Part1()
    {
        var input = Input.Lines().Select(x => x.Split("|")[1].Trim().Split(" ").Count(y => new[] { 2, 3, 4, 7 }.Contains(y.Length))).Sum();

        return input;
    }

    public override Output Part2()
    {
        var input = Input.Lines().Select(x => x.Split("|").Select(x => x.Trim()).ToArray()).ToArray();
        var answer = 0;
        foreach (var line in input)
        {
            var l = line[0].Split(' ');
            var r = line[1].Split(' ');
            l = l.Select(x => new string(x.OrderBy(c => c).ToArray())).ToArray();
            r = r.Select(x => new string(x.OrderBy(c => c).ToArray())).ToArray();

            var one = l.First(x => x.Length == 2);
            var four = l.First(x => x.Length == 4);
            var seven = l.First(x => x.Length == 3);
            var eight = l.First(x => x.Length == 7);


            var nine = l.Single(x => x.Length == 6 && x.Intersect(one).Count() == 2 && x.Intersect(four).Count() == 4);
            var six = l.Single(x => x.Length == 6 && x.Intersect(one).Count() == 1);

            var zero = l.Single(x => x.Length == 6 && !new[] { six, nine }.Contains(x));
            var three = l.Single(x => x.Length == 5 && x.Intersect(one).SequenceEqual(one));

            var two = l.Single(x => x.Length == 5 && x != three && x.Intersect(four).Count() == 2);
            var five = l.Single(x => x.Length == 5 && x != three && x.Intersect(four).Count() == 3);

            var ns = new[] { zero, one, two, three, four, five, six, seven, eight, nine };

            var test = Int32.Parse(string.Join("", r.Select(x => ns.IndexOf(x).ToString())));
            answer += test;
        }

        return answer;
    }
}
