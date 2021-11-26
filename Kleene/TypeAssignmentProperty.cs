namespace Kleene;

public class TypeAssignmentProperty
{
    internal class Model : IModel<TypeAssignmentProperty>
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

    public string Name { get; }
    public TextValueExpression Value { get; }

    public TypeAssignmentProperty(string name, TextValueExpression value)
    {
        Name = name;
        Value = value;
    }
}
