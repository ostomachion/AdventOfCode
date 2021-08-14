using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class CaptureExpression : Expression
    {
        public string Name { get; }
        public Expression Expression { get; }

        public CaptureExpression(string name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            context.CaptureTree.Open(Name);
            foreach (var result in Expression.Run(context))
            {
                context.CaptureTree.Close(result);
                yield return result;
                context.CaptureTree.Unclose();
            }
            context.CaptureTree.Unopen();
        }
    }
}
