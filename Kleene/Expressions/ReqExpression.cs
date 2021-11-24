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
}
