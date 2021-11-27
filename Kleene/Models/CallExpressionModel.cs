namespace Kleene.Models;

public class CallExpressionModel : IModel<CallExpression>
{
    public string? Name { get; set; }
    public string? CaptureName { get; set; }

    public CallExpression Convert()
    {
        if (Name is null)
            throw new InvalidOperationException();

        return new(Name, CaptureName);
    }
}