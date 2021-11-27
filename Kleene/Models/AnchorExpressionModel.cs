namespace Kleene.Models;

public class AnchorExpressionModel : IModel<AnchorExpression>
{
    public AnchorModel? Anchor { get; set; }

    public AnchorExpression Convert()
    {
        if (Anchor is null)
            throw new InvalidOperationException();

        return new(Anchor.Convert());
    }
}
