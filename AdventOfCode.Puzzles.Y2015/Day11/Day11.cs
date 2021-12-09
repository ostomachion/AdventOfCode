using System.Text;

namespace AdventOfCode.Puzzles.Y2015.Days.Day11;

public class Day11 : Day
{
    public override Output Part1()
    {
        var password = Input;
        do
        {
            password = Increment(password);
        } while (!Check(password));
        return password;
    }

    public override Output Part2()
    {
        var password = Input;
        for (var i = 0; i < 2; i++)
        {
            do
            {
                password = Increment(password);
            } while (!Check(password));
        }
        return password;
    }

    public static string Increment(string password)
    {
        if (password.Length == 1)
            return ((char)(password[0] + 1)).ToString();
        else if (password[^1] == 'z')
            return Increment(password[0..^1]) + "a";
        else
            return password[0..^1] + (char)(password[^1] + 1);
    }

    public static bool Check(string password)
    {
        int run = 0;
        int maxRun = 0;
        char last = '\0';
        HashSet<char> doubles = new();
        foreach (var c in password)
        {
            if (c == last + 1)
                run++;
            else
                run = 1;
            maxRun = Math.Max(run, maxRun);

            if (c is 'i' or 'o' or 'l')
                return false;

            if (c == last)
                doubles.Add(c);

            last = c;
        }

        return maxRun >= 3 && doubles.Count >= 2;
    }
}
