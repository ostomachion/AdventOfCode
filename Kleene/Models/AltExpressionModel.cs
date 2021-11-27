namespace Kleene.Models;

public class AltExpressionModel : IModel<AltExpression>
{
    public List<IModel<Expression>>? Expressions { get; set; }

    public AltExpression Convert()
    {
        if (Expressions is null)
            throw new InvalidOperationException();

        return new(Expressions.Select(x => x.Convert()).ToArray());
    }
}