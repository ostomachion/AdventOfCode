using System.Text.RegularExpressions;

namespace Kleene;

public class TransformExpression : Expression
{
    public Expression Input { get; }
    public Expression Output { get; }

    public TransformExpression(Expression input, Expression output)
    {
        Input = input;
        Output = output;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        if (!context.Local.Producing || !context.Local.Consuming)
        {
            throw new InvalidOperationException("Cannot nest transform expressions.");
        }

        context.Local.Producing = false;
        foreach (var inputResult in Input.Run(context))
        {
            context.Local.Producing = true;
            context.Local.Consuming = false;
            foreach (var outputResult in Output.Run(context))
            {
                context.Local.Producing = true;
                context.Local.Consuming = true;
                yield return new(inputResult.Input, outputResult.Output);
                context.Local.Consuming = false;
            }
            context.Local.Producing = false;
            context.Local.Consuming = true;
        }
        context.Local.Producing = true;
    }

    public override string ToString()
    {
        var input = Input.ToString()!;
        var output = Output.ToString()!;
        var spaces = Input is not TextExpression or CharacterClassExpression && input.Any(" \t".Contains) || Output is not TextExpression or CharacterClassExpression && output.Any(" \t".Contains);
        var emptyLines = Regex.IsMatch(input, "\n[ \t]*\n") || Regex.IsMatch(output, "\n[ \t]*\n");
        if (input.Contains('\n') || output.Contains('\n') || input.Length + output.Length + (spaces ? 1 : 3) > ToStringLength)
        {
            return $"{input}\n{(emptyLines ? "\n/\n" : "/")}\n{output}";
        }
        else
        {
            return $"{input}{(spaces ? " / " : "/")}{output}";
        }
    }
}
