using System.Collections.Generic;

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
}
