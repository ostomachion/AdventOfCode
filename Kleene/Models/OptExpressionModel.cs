namespace Kleene.Models;

public class OptExpressionModel : IModel<OptExpression>
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

