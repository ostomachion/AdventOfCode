using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class CaptureExpression : Expression
    {
        public CaptureName Name { get; }
        public Expression Expression { get; }

        public CaptureExpression(CaptureName name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            context.CaptureTree.Open(Name);
            foreach (var result in Expression.Run(context))
            {
                context.CaptureTree.Close(Name, result);
                yield return result;
                context.CaptureTree.Unclose(Name);
            }
            context.CaptureTree.Unopen(Name);
        }
    }
}
