namespace Kleene;

public class AtomicExpression : Expression
{
    internal class Model : IModel<AtomicExpression>
    {
        public IModel<Expression>? Expression { get; set; }

        public AtomicExpression Convert()
        {
            if (Expression is null)
                throw new InvalidOperationException();

            return new(Expression.Convert());
        }
    }

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
