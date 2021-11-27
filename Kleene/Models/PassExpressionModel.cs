namespace Kleene.Models;

public class PassExpressionModel : IModel<PassExpression>
{
    public PassExpression Convert() => new();
}
