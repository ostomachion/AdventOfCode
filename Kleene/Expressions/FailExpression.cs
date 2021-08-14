using System.Collections.Generic;

namespace Kleene
{
    public class FailExpression : Expression
    {
        public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
        {
            yield break;
        }
    }
}
