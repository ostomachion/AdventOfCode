namespace Kleene;

public class TypeAssignmentProperty
{
    public string Name { get; }
    public TextValueExpression Value { get; }

    public TypeAssignmentProperty(string name, TextValueExpression value)
    {
        Name = name;
        Value = value;
    }
}
