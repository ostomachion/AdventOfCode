namespace Kleene.Models;

public class RepExpressionModel : IModel<RepExpression>
{
    public IModel<Expression>? Expression { get; set; }
    public IModel<Expression>? Separator { get; set; }
    public RepCountModel? Count { get; set; }
    public MatchOrder? Order { get; set; }

    public RepExpression Convert()
    {
        if (Expression is null)
            throw new InvalidOperationException();

        return Order is not null ? new(Expression.Convert(), Separator?.Convert(), Count?.Convert() ?? new(), Order.Value)
            : new(Expression.Convert(), Separator?.Convert(), Count?.Convert() ?? new());
    }
}
