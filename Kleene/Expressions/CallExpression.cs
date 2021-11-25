namespace Kleene;

public class CallExpression : Expression
{
    public string Name { get; }
    public CaptureName? CaptureName { get; }

    public CallExpression(string name, CaptureName? captureName = null)
    {
        Name = name;
        CaptureName = captureName;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        if (context.FunctionList[Name] is not Expression expression)
        {
            yield break;
        }

        var captureName = CaptureName ?? new("!F");
        context.CaptureTree.Open(captureName);
        context.CaptureTree.Current.IsFunctionBoundary = true;
        context.CallStack.Push(new(Name, expression));
        foreach (var result in expression.Run(context))
        {
            context.CallStack.Pop();
            context.CaptureTree.Close(captureName, result);
            yield return result;
            context.CaptureTree.Unclose(captureName);
            context.CallStack.Push(new(Name, expression));
        }
        context.CallStack.Pop();
        context.CaptureTree.Unopen(captureName);
    }
}
