namespace Kleene;

public class TypeAssignmentExpression : Expression
{
    public string TypeName { get; }
    public IEnumerable<TypeAssignmentProperty> Properties { get; }

    public TypeAssignmentExpression(string typeName, params TypeAssignmentProperty[] properties)
    {
        TypeName = typeName;
        Properties = properties;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        context.CaptureTree.Open("!T");
        context.CaptureTree.Set("FullName", new(context.ResolveTypeName(TypeName).FullName!));
        context.CaptureTree.Open("Properties");
        foreach (var property in Properties.OrderBy(x => x.Name))
        {
            ExpressionResult? value = property.Value.GetValue(context);
            if (value is not null)
            {
                context.CaptureTree.Set(property.Name, value);
            }
        }
        context.CaptureTree.Close("Properties", new());
        context.CaptureTree.Close("!T", new());

        yield return new();

        context.CaptureTree.Unclose("!T");
        context.CaptureTree.Unclose("Properties");
        foreach (var property in Properties.OrderByDescending(x => x.Name))
        {
            ExpressionResult? value = property.Value.GetValue(context);
            if (value is not null)
            {
                context.CaptureTree.Unset(property.Name);
            }
        }
        context.CaptureTree.Unopen("Properties");
        context.CaptureTree.Unset("FullName");
        context.CaptureTree.Unopen("!T");
    }

    public override string ToString()
    {
        var value = "::" + TypeName;
        if (Properties.Any())
        {
            value += " {\n";
            value += String.Join(", ", Properties.Select(x => $"  {x.Name} = {x.Value}\n"));
            value += "}";
        }
        return value;
    }
}
