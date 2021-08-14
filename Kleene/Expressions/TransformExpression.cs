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
            bool consuming = context.Local.Consuming;
            bool producing = context.Local.Producing;
            context.Local.Producing = false;
            foreach (var inputResult in Input.Run(context))
            {
                context.Local.Producing = producing;
                context.Local.Consuming = false;
                foreach (var outputResult in Output.Run(context))
                {
                    context.Local.Producing = producing;
                    context.Local.Consuming = consuming;
                    yield return new ExpressionResult(inputResult.Input, outputResult.Output);
                    context.Local.Consuming = false;
                }
                context.Local.Producing = false;
                context.Local.Consuming = consuming;
            }
            context.Local.Producing = producing;
        }
    }
}
