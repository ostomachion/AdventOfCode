using System;
using System.Collections.Generic;

namespace Kleene
{
    public class TransformExpression : Expression
    {
        public Expression Input { get; }
        public Expression Output { get; }

        public TransformExpression(Expression input, Expression output)
        {
            Input = input;
            Output = output;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            if (!context.Local.Producing || !context.Local.Consuming)
                throw new InvalidOperationException("Cannot nest transform expressions.");
                
            context.Local.Producing = false;
            foreach (var inputResult in Input.Run(context))
            {
                context.Local.Producing = true;
                context.Local.Consuming = false;
                foreach (var outputResult in Output.Run(context))
                {
                    context.Local.Producing = true;
                    context.Local.Consuming = true;
                    yield return new ExpressionResult(inputResult.Input, outputResult.Output);
                    context.Local.Consuming = false;
                }
                context.Local.Producing = false;
                context.Local.Consuming = true;
            }
            context.Local.Producing = true;
        }
    }
}
