namespace Kleene.Models;

public class RatchetExpressionModel : IModel<RatchetExpression>
{
    public RatchetExpression Convert() => new();
}
