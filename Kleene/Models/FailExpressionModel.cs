namespace Kleene.Models;

public class FailExpressionModel : IModel<FailExpression>
{
    public FailExpression Convert() => new();
}
