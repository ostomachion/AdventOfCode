namespace Kleene.Models;

public class CaptureExpressionModel : IModel<CaptureExpression>
{
    public string? Name { get; set; }
    public IModel<Expression>? Expression { get; set; }

    public CaptureExpression Convert()
    {
        if (Name is null || Expression is null)
            throw new InvalidOperationException();

        return new(Name, Expression.Convert());
    }
}
