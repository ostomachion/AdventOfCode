namespace AdventOfCode.Puzzles.Y2021.Days.Day03;

public class Day03 : Day
{
    public override Output Part1()
    {
        var value = Input.Lines().Parse<string>(@"[01]+").ToArray();

        var answer = "";
        var answer2 = "";
        int count = value.Count();
        for (int i = 0; i < value[0].Length; i++)
        {
            answer += value.Select(x => x[i]).ToArray().Count(x => x == '0') > count / 2 ? "1" : "0";
            answer2 += value.Select(x => x[i]).ToArray().Count(x => x == '0') > count / 2 ? "0" : "1";
        }

        var x = Convert.ToInt64(answer, 2);
        var y = Convert.ToInt64(answer2, 2);

        return x * y;
    }

    public override Output Part2()
    {
        var value = Input.Lines().Parse<string>(@"[01]+").ToArray();

        int count = value.Count();
        int n = value[0].Length;
        int i;
        string answer = "";
        string answer2 = "";
        for (i = 0; i < n; i++)
        {
            var c = value.Select(x => x[i]).ToArray().Count(x => x == '0') > count / 2 ? '0' : '1';
            value = value.Where(x => x[i] == c).ToArray();
            count = value.Count();
            answer += c.ToString();
            if (value.Length == 1)
            {
                answer = value[0];
                break;
            }
        }

        value = Input.Lines().Parse<string>(@"[01]+").ToArray();
        count = value.Count();
        int j;
        for (j = 0; j < n; j++)
        {
            var c = value.Select(x => x[j]).ToArray().Count(x => x == '0') <= count / 2 ? '0' : '1';
            value = value.Where(x => x[j] == c).ToArray();
            count = value.Count(); answer2 += c.ToString();
            if (value.Length == 1)
            {
                answer2 = value[0];
                break;
            }
        }

        var x = Convert.ToInt64(answer, 2);
        var y = Convert.ToInt64(answer2, 2);

        return x * y;
    }
}
