using System.Collections.Generic;

namespace Kleene
{
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
                yield break;

            var captureName = CaptureName ?? new("!F");
            context.CaptureTree.Open(captureName);
            context.CaptureTree.Current.IsFunctionBoundary = true;
            foreach (var result in expression.Run(context))
            {
                context.CaptureTree.Close(captureName, result);
                yield return result;
                context.CaptureTree.Unclose(captureName);
            }
            context.CaptureTree.Unopen(captureName);

            context.Ratchet = false;
        }
    }
}
