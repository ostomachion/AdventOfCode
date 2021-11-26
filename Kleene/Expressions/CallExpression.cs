namespace Kleene;

public class CallExpression : Expression
{
    internal class Model : IModel<CallExpression>
    {
        public string? Name { get; set; }
        public string? CaptureName { get; set; }

        public CallExpression Convert()
        {
            if (Name is null)
                throw new InvalidOperationException();

            return new(Name, CaptureName);
        }
    }

    public string Name { get; }
    public CaptureName? CaptureName { get; }

    public CallExpression(string name, CaptureName? captureName = null)
    {
        Name = name;
        CaptureName = captureName;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        if (context.GetFunction(Name) is not Expression expression)
        {
            yield break;
        }

        var captureName = CaptureName ?? new("!F");
        context.CaptureTree.Open(captureName);
        context.CaptureTree.Current!.IsFunctionBoundary = true;
        context.CallStack.Push(new(Name));
        foreach (var result in expression.Run(context))
        {
            var frame = context.CallStack.Pop();
            context.CaptureTree.Close(captureName, result);
            yield return result;
            context.CaptureTree.Unclose(captureName);
            frame.Ratchet = false;
            context.CallStack.Push(frame);
        }
        context.CallStack.Pop();
        context.CaptureTree.Unopen(captureName);
    }
}
