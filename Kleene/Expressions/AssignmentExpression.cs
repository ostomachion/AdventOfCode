namespace Kleene;

public class AssignmentExpression : Expression
{
    internal class Model : IModel<AssignmentExpression>
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

    public CaptureName Name { get; }
    public TextValueExpression Value { get; }

    public AssignmentExpression(CaptureName name, TextValueExpression value)
    {
        Name = name;
        Value = value;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        var value = Value.GetValue(context);
        if (value is null)
        {
            yield break;
        }

        context.CaptureTree.Set(Name, value);
        yield return new();
        context.CaptureTree.Unset(Name);
    }
}
