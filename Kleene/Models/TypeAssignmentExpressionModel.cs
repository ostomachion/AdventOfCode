namespace Kleene.Models;

public class TypeAssignmentExpressionModel : IModel<TypeAssignmentExpression>
{
    public string? TypeName { get; set; }
    public List<TypeAssignmentPropertyModel>? Properties { get; set; }

    public TypeAssignmentExpression Convert()
    {
        if (TypeName is null)
            throw new InvalidOperationException();

        return new(TypeName, Properties?.Select(x => x.Convert()).ToArray() ?? Array.Empty<TypeAssignmentProperty>());
    }
}
