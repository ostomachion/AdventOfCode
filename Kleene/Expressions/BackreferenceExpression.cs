using System.Collections.Generic;

namespace Kleene;

public class BackreferenceExpression : TextValueExpression
{
    public CaptureName Name { get; }

    public BackreferenceExpression(CaptureName name)
    {
        Name = name;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        var capture = GetValue(context);
        if (capture is null)
        {
            yield break;
        }

        // TODO: Should this be rewritten?
        var expression = new TransformExpression(new TextExpression(capture.Input), new TextExpression(capture.Output));
        foreach (var result in expression.Run(context))
        {
            yield return result;
        }
    }

    public override ExpressionResult? GetValue(ExpressionContext context) => context.CaptureTree[Name]?.Value;
}
