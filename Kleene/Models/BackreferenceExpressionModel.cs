namespace Kleene.Models;

public class BackreferenceExpressionModel : IModel<BackreferenceExpression>
{
    public string? Name { get; set; }

    public BackreferenceExpression Convert()
    {
        if (Name is null)
            throw new InvalidOperationException();

        return new(Name);
    }
}