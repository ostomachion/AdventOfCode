using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AssignmentExpression : Expression
    {
        public CaptureName Name { get; }
        public TextValueExpression Value { get; }

        public AssignmentExpression(CaptureName name, TextValueExpression value)
        {
            Name = name;
            Value = value;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            var value = Value.GetValue(context);
            if (value is null)
                yield break;

            context.CaptureTree.Open(Name);
            context.CaptureTree.Close(Name, value);
            yield return new ExpressionResult("");
            context.CaptureTree.Unclose(Name);
            context.CaptureTree.Unopen(Name);
        }
    }
}
