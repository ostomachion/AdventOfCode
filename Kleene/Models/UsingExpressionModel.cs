namespace Kleene.Models;

public class UsingExpressionModel : IModel<UsingExpression>
{
    public string? NamespaceName { get; set; }

    public UsingExpression Convert()
    {
        if (NamespaceName is null)
            throw new InvalidOperationException();

        return new(NamespaceName);
    }
}
