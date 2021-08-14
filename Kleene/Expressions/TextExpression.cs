using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public class TextExpression : TextlikeExpression
    {
        public string Value { get; }

        private readonly Expression expression;

        public TextExpression(string value)
        {
            Value = value;
            this.expression = new ConcatExpression(value.Select(c => new CharExpression(c)));
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            // TODO: Make this more efficient.
            foreach (var result in this.expression.Run(context))
            {
                yield return result;
            }
        }
    }
}