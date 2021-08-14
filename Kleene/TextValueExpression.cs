using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public abstract class TextValueExpression : Expression
    {
        public abstract ExpressionResult? GetValue(ExpressionContext context);
    }
}