namespace Kleene.Models;

public class ConcatExpressionModel : IModel<ConcatExpression>
{
    public List<IModel<Expression>>? Expressions { get; set; }

    public ConcatExpression Convert()
    {
        if (Expressions is null)
            throw new InvalidOperationException();

        return new(Expressions.Select(x => x.Convert()).ToArray());
    }
}
