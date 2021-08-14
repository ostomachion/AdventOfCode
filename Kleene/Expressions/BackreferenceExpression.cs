using System.Collections.Generic;

namespace Kleene
{
    public class BackreferenceExpression : TextlikeExpression
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
}
