namespace Kleene;

public class FunctionExpression : Expression
{
    public string Name { get; }
    public Expression Expression { get; }

    public FunctionExpression(string name, Expression expression)
    {
        Name = name;
        Expression = expression;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        context.DefineFunction(Name, Expression);
        yield return new();
        context.UndefineFunction(Name);
    }

    public override string ToString()
    {
        var value = $"<{Name}> {{";
        var text = Expression.ToString()!;
        if (text.Contains('\n') || text.Length + value.Length + 3 > ToStringLength)
        {
            value += "\n  " + text.Replace("\n", "\n  ") + "\n}";
        }
        else
        {
            value += $" {text} }}";
        }

        return value;
    }
}
