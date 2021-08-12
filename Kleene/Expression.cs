using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public abstract class Expression
    {
        public string? Transform(string input)
        {
            var context = new ExpressionContext(input);
            foreach (var result in Run(context))
            {
                if (context.IsAtEnd)
                    return result.Output;
            }
            return null;
        }

        public abstract IEnumerable<ExpressionResult> Run(ExpressionContext context);
    }
}
