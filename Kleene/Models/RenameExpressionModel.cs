namespace Kleene.Models;

public class RenameExpressionModel : IModel<RenameExpression>
{
    public string? Name { get; set; }
    public string? NewName { get; set; }

    public RenameExpression Convert()
    {
        if (Name is null || NewName is null)
            throw new InvalidOperationException();

        return new(Name, NewName);
    }
}

