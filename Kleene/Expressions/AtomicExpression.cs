namespace Kleene;

public class AtomicExpression : Expression
{
    public Expression Expression { get; }

    public AtomicExpression(Expression expression)
    {
        Expression = expression;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        foreach (var result in Expression.Run(context))
        {
            yield return result;
            yield break;
        }
    }
    public override string ToString()
    {
        string value = Expression.ToString()!.Replace("\n", "\n  ");
        if (value.Contains('\n') || value.Length + 3 > ToStringLength)
            value = "\n  " + value.Replace("\n", "\n  ") + "\n";
        return $"(>{value})";
    }
}
