using System.Collections.Generic;

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
}
