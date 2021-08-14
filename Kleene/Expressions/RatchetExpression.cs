using System;
using System.Collections.Generic;

namespace Kleene
{
    public class RatchetExpression : Expression
    {
        public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
        {
            yield return new();

            // TODO:
            throw new NotImplementedException();
        }
    }
}
