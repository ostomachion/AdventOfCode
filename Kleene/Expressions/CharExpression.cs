using System.Collections.Generic;

namespace Kleene
{
    public class CharExpression : Expression
    {
        public char Value { get; }

        public CharExpression(char value)
        {
            Value = value;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            if (context.Local.Consuming && context.Local.IsAtEnd)
                yield break;

            if (!context.Local.Consuming || context.Local.Input[context.Local.Index] == Value)
            {
                context.Consume(1);
                context.Produce(Value.ToString());
                yield return new ExpressionResult(Value.ToString());
                context.Unproduce();
                context.Unconsume(1);
            }
        }
    }
}
