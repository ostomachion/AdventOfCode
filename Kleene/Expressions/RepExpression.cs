using System;
using System.Collections.Generic;

namespace Kleene
{
    public class RepExpression : Expression
    {
        public Expression Expression { get; }
        public RepCount Count { get; }
        public MatchOrder Order { get; }

        public RepExpression(Expression expression, RepCount count, MatchOrder order = MatchOrder.Greedy)
        {
            Expression = expression;
            Count = count;
            Order = order;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
