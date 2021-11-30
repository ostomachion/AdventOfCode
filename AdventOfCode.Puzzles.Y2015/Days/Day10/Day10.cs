using System.Text;

namespace AdventOfCode.Puzzles.Y2015.Days.Day10;
public record N(List<string> Items);
public class Day10 : Day
{
    public override Output Part1()
    {
        var text = Input;
        var expression = Expression.Parse(@"(
        - 333 / 33 - 33 / 23 - 3 / 13
        - 222 / 32 - 22 / 22 - 2 / 12
        - 111 / 31 - 11 / 21 - 1 / 11
        )*");
        for (var i = 0; i < 50; i++)
        {
            Console.WriteLine(i);
            text = expression.Parse<string>(text);
        }
        return text.Length;
    }

    public override Output Part2()
    {
        var text = Input;
        var expression = Expression.Parse(@"(
        - 333 / 33 - 33 / 23 - 3 / 13
        - 222 / 32 - 22 / 22 - 2 / 12
        - 111 / 31 - 11 / 21 - 1 / 11
        )*");
        for (var i = 0; i < 50; i++)
        {
            Console.WriteLine(i);
            text = expression.Parse<string>(text);
        }
        return text.Length;
    }
}
