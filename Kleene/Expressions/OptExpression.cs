namespace Kleene;

public class OptExpression : Expression
{
    internal class Model : IModel<OptExpression>
    {
        public IModel<Expression>? Expression { get; set; }
        public MatchOrder? Order { get; set; }

        public OptExpression Convert()
        {
            if (Expression is null || Order is null)
                throw new InvalidOperationException();

            return new(Expression.Convert(), Order.Value);
        }
    }

    public Expression Expression { get; }
    public MatchOrder Order { get; }

    public OptExpression(Expression expression, MatchOrder order = MatchOrder.Greedy)
    {
        Expression = expression;
        Order = order;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        if (Order is MatchOrder.Lazy)
        {
            yield return new();
        }

        foreach (var result in Expression.Run(context))
        {
            yield return result;
        }

        if (Order is MatchOrder.Greedy)
        {
            yield return new();
        }
    }
}
