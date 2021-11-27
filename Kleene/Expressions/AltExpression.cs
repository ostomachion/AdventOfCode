namespace Kleene;

public class AltExpression : Expression
{
    public IEnumerable<Expression> Expressions { get; }

    public AltExpression(params Expression[] expressions)
    {
        Expressions = expressions;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        foreach (var expression in Expressions)
        {
            foreach (var result in expression.Run(context))
            {
                yield return result;
            }
        }
    }
}
