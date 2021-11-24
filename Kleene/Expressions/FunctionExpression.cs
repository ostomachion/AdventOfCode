using System.Collections.Generic;

namespace Kleene;

public class FunctionExpression : Expression
{
    public string Name { get; }
    public Expression Value { get; }

    public FunctionExpression(string name, Expression value)
    {
        Name = name;
        Value = value;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        context.FunctionList.Define(Name, Value);
        yield return new();
        context.FunctionList.Undefine(Name);
    }
}
