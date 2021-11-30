using System.Text;

namespace AdventOfCode.Puzzles.Y2015.Days.Day10;
public record N(List<string> Items);
public class Day10 : Day
{
    public override Output Part1()
    {
        var text = Input;
        var expression = Expression.Parse(@"((\d:a (@a)* ;):Items)*");
        for (var i = 0; i < 40; i++)
        {
            var parsed = expression.Parse<N>(text);
            text = String.Join("", parsed.Items.Select(x => x.Length.ToString() + x[0]));
        }
        return text.Length;
    }

    public override Output Part2()
    {
        var text = Input;
        var expression = Expression.Parse(@"((\d:a (@a)* ;):Items)*");
        for (var i = 0; i < 50; i++)
        {
            Console.WriteLine(i);
            var parsed = expression.Parse<N>(text);
            var sb = new StringBuilder();
            foreach (var item in parsed.Items)
            {
                sb.Append(item.Length);
                sb.Append(item[0]);
            }
            text = sb.ToString();
        }
        return text.Length;
    }
}
