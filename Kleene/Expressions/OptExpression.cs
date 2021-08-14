using System.Collections.Generic;

namespace Kleene
{
    public class OptExpression : Expression
    {
        public Expression Expression { get; }
        public MatchOrder Order { get; }

        public OptExpression(Expression expression, MatchOrder order = MatchOrder.Greedy)
        {
            Expression = expression;
            Order = order;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            if (Order is MatchOrder.Greedy)
                yield return new();
            
            foreach (var result in Expression.Run(context))
            {
                yield return result;
            }

            if (Order is MatchOrder.Lazy)
                yield return new();
        }
    }
}
