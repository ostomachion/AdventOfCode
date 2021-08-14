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

        public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
        {
            if (context.FunctionList[Name] is not Expression expression)
                yield break;

            foreach (var result in expression.Run(context))
            {
                yield return result;
            }

            context.Ratchet = false;
        }
    }
}
