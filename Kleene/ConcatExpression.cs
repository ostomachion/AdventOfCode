using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    // TODO: Make this more efficient.
    public class ConcatExpression : Expression
    {
        public IEnumerable<Expression> Expressions { get; }

        public ConcatExpression(IEnumerable<Expression> expressions)
        {
            Expressions = expressions;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            if (!Expressions.Any())
                yield return new ExpressionResult("");

            var head = Expressions.First();
            var tail = new ConcatExpression(Expressions.Skip(1));
            foreach (var headResult in head.Run(context))
            {
                foreach (var tailResult in tail.Run(context))
                {
                    yield return new ExpressionResult(headResult.Input + tailResult.Input, headResult.Output + tailResult.Output);
                }
            }
        }
    }
}
