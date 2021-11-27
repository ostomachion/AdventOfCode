namespace Kleene;

public class AssignmentExpression : Expression
{
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

    public override string ToString()
    {
        string name = Name.ToString();
        string value = Value.ToString()!.Replace("\n", "\n  ");
        if (value.Contains('\n') || value.Length + name.Length + 6 > ToStringLength)
            value = "\n  " + value.Replace("\n", "\n  ") + "\n";
        return $"(:{name} = {value})";
    }
}
