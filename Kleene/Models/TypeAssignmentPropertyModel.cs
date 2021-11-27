namespace Kleene.Models;

public class TypeAssignmentPropertyModel : IModel<TypeAssignmentProperty>
{
    public string? Name { get; set; }
    public IModel<TextValueExpression>? Value { get; set; }

    public TypeAssignmentProperty Convert()
    {
        if (Name is null || Value is null)
            throw new InvalidOperationException();

        return new(Name, Value.Convert());
    }
}
