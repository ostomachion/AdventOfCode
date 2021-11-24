using System.Collections.Generic;

namespace Kleene;

public class PassExpression : Expression
{
    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        yield return new();
    }
}
