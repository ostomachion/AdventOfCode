namespace Kleene;

public class ReqExpression : Expression
{
    public Expression Expression { get; }

    public ReqExpression(Expression expression)
    {
        Expression = expression;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        foreach (var result in Expression.Run(context))
        {
            if (result.Input.Length != 0)
            {
                yield return result;
            }
        }
    }

    public override string ToString()
    {
        var value = Expression.ToString()!;
        if (value.Contains('\n') || value.Length + 3 > ToStringLength)
            value = "\n  " + value.Replace("\n", "\n  ") + "\n";
        return $"({value})!";
    }
}
