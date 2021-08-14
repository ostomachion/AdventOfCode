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
            bool consuming = context.Consuming;
            bool producing = context.Producing;
            context.Producing = false;
            foreach (var inputResult in Input.Run(context))
            {
                context.Producing = producing;
                context.Consuming = false;
                foreach (var outputResult in Output.Run(context))
                {
                    context.Producing = producing;
                    context.Consuming = consuming;
                    yield return new ExpressionResult(inputResult.Input, outputResult.Output);
                    context.Consuming = false;
                }
                context.Producing = false;
                context.Consuming = consuming;
            }
            context.Producing = producing;
        }
    }
}
