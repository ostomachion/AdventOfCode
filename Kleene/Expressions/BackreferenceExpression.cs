using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public class BackreferenceExpression : Expression
    {
        public CaptureName Name { get; }

        public BackreferenceExpression(CaptureName name)
        {
            Name = name;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            var capture = context.CaptureTree[Name]?.Value;
            if (capture is null)
                yield break;

            // TODO: Should this be rewritten?
            var expression = new TransformExpression(new TextExpression(capture.Input), new TextExpression(capture.Output));
            foreach (var result in expression.Run(context))
            {
                yield return result;
            }
        }
    }

    public class TextExpression : Expression
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
