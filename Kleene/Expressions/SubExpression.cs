using System;
using System.Collections.Generic;

namespace Kleene
{
    public class SubExpression : Expression
    {
        public TextValueExpression Input { get; }
        public Expression Expression { get; }

        public SubExpression(TextValueExpression input, Expression expression)
        {
            Input = input;
            Expression = expression;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            var sub = Input.GetValue(context);
            if (sub is null)
                yield break;

            var originalContext = context.Local;
            var subContext = new ExpressionLocalContext(sub.Input);

            context.Local = subContext;
            foreach (var _ in Expression.Run(context))
            {
                context.Local = originalContext;
                yield return new();
                context.Local = subContext;
            }
            context.Local = originalContext;
        }
    }
}
