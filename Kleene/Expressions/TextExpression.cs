using System.Text;
using System.Text.RegularExpressions;

namespace Kleene;

public class TextExpression : TextValueExpression
{
    public string Value { get; }

    public TextExpression(string value)
    {
        Value = value;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        var remaining = context.Local.Input.AsSpan()[context.Local.Index..];
        if (!context.Local.Consuming || remaining.StartsWith(Value))
        {
            context.Consume(Value.Length);
            context.Produce(Value);
            yield return new(Value);
            context.Unproduce();
            context.Unconsume(Value.Length);
        }
    }

    public override ExpressionResult? GetValue(ExpressionContext context) => new(Value);

    public override string ToString()
    {
        if (new Regex(@"^[A-Za-z0-9]+$").IsMatch(Value))
            return Value;

        var builder = new StringBuilder();
        foreach (var c in Value)
        {
            builder.Append(c switch
            {
                '\'' => "[']",
                '[' => "[<]",
                ']' => "[>]",
                '\n' => "[n]",
                '\t' => "[t]",
                _ => c.ToString()
            });
        }

        return $"'{builder}'";
    }

    public static implicit operator TextExpression(string value) => new(value);
}
