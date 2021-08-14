using System.Collections.Generic;

namespace Kleene
{
    public class CallExpression : Expression
    {
        public string Name { get; }

        public CallExpression(string name)
        {
            Name = name;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            if (context.FunctionList[Name] is Expression expression)
            {
                foreach (var result in expression.Run(context))
                {
                    yield return result;
                }
            }
        }
    }
}
