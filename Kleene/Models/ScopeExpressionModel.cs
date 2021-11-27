namespace Kleene.Models;

public class ScopeExpressionModel : IModel<ScopeExpression>
{
    public string? Name { get; set; }
    public IModel<Expression>? Expression { get; set; }

    public ScopeExpression Convert()
    {
        if (Name is null || Expression is null)
            throw new InvalidOperationException();

        return new(Name, Expression.Convert());
    }
}