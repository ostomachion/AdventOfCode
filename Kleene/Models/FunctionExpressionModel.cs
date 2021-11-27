namespace Kleene.Models;

public class FunctionExpressionModel : IModel<FunctionExpression>
{
    public string? Name { get; set; }
    public IModel<Expression>? Expression { get; set; }

    public FunctionExpression Convert()
    {
        if (Name is null || Expression is null)
            throw new InvalidOperationException();

        return new(Name, Expression.Convert());
    }
}
