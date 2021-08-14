using System.Collections.Generic;

namespace Kleene
{
    public class FunctionExpression : Expression
    {
        public string Name { get; }
        public Expression Value { get; }

        public FunctionExpression(string name, Expression value)
        {
            Name = name;
            Value = value;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            context.FunctionList.Define(Name, Value);
            yield return new ExpressionResult("");
            context.FunctionList.Undefine(Name);
        }
    }
}
