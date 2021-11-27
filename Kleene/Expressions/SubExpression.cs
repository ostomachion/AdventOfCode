namespace Kleene;

public class SubExpression : Expression
{
    public TextValueExpression Input { get; }
    public Expression? Expression { get; }

    public SubExpression(TextValueExpression input, Expression? expression = null)
    {
        if (input is TextExpression && expression is null)
        {
            throw new ArgumentException("A sub-expression on a text expression must have an expression to match against.", nameof(input));
        }

        Input = input;
        Expression = expression;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        var sub = Input.GetValue(context);
        if (sub is null)
        {
            yield break;
        }

        // If no expression is given, pass if the input value exists.
        if (Expression is null)
        {
            yield return new();
            yield break;
        }

        var originalContext = context.Local;
        var subContext = new ExpressionLocalContext(sub.Input);

        context.Local = subContext;
        foreach (var _ in Expression.Run(context))
        {
            context.Local = originalContext;
            yield return new();
            context.Local = subContext;
        }
        context.Local = originalContext;
    }

    public override string ToString()
    {
        var input = Input.ToString()!;

        if (Expression is null)
        {
            return $"(?{input})";
        }
        else
        {
            string value = Expression.ToString()!.Replace("\n", "\n  ");
            if (value.Contains('\n') || value.Length + input.Length + 4 > ToStringLength)
                value = "\n  " + value.Replace("\n", "\n  ") + "\n";
            return $"(?{input} {value})";
        }
    }
}
