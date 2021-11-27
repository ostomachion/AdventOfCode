namespace Kleene.Models;

public class AssignmentExpressionModel : IModel<AssignmentExpression>
{
    public string? Name { get; set; }
    public IModel<TextValueExpression>? Value { get; set; }

    public AssignmentExpression Convert()
    {
        if (Name is null || Value is null)
            throw new InvalidOperationException();

        return new(Name, Value.Convert());
    }
}