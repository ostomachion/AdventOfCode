namespace AdventOfCode.Puzzles.Y2015.Days;

public class Day05 : Day
{
    public override Output Part1()
    {
        return Input.Lines().Count(x =>
            Regex.IsMatch(x, "[aeiou].*[aeiou].*[aeiou]") && Regex.IsMatch(x, @"(.)\1") &&
                !x.Contains("ab") && !x.Contains("cd") && !x.Contains("pq") && !x.Contains("xy"));
    }

    public override Output Part2()
    {
        return Input.Lines().Count(x =>
            Regex.IsMatch(x, @"(.).\1") && Regex.IsMatch(x, @"(..).*\1"));
    }
}
