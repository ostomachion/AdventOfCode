namespace Kleene.Models;

public class AtomicExpressionModel : IModel<AtomicExpression>
{
    public IModel<Expression>? Expression { get; set; }

    public AtomicExpression Convert()
    {
        if (Expression is null)
            throw new InvalidOperationException();

        return new(Expression.Convert());
    }
}