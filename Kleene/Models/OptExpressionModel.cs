namespace Kleene.Models;

public class OptExpressionModel : IModel<OptExpression>
{
    public IModel<Expression>? Expression { get; set; }
    public MatchOrder? Order { get; set; }

    public OptExpression Convert()
    {
        if (Expression is null)
            throw new InvalidOperationException();

        return Order is not null ? new(Expression.Convert(), Order.Value)
            : new(Expression.Convert());
    }
}
