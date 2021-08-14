using System.Collections.Generic;

namespace Kleene
{
    public class FailExpression : Expression
    {
        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            yield break;
        }
    }
}
