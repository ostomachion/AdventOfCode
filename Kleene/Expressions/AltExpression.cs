using System.Text.RegularExpressions;

namespace Kleene;

public class AltExpression : Expression
{
    public IEnumerable<Expression> Expressions { get; }

    public AltExpression(params Expression[] expressions)
    {
        Expressions = expressions;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        foreach (var expression in Expressions)
        {
            foreach (var result in expression.Run(context))
            {
                yield return result;
            }
        }
    }

    public override string ToString()
    {
        var expressions = Expressions.Select(x => (Expression: x, Text: x.ToString()!)).ToArray();

        var spaces = expressions.Any(x => x.Text.Length > 16 || x.Expression is not TextExpression or CharacterClassExpression && x.Text.Any(" \t".Contains));

        if (expressions.Any(x => x.Text.Contains('\n')) || expressions.Sum(x => x.Text.Length) + (spaces ? 1 : 3) * (expressions.Length - 1) > ToStringLength)
        {
            return string.Join("\n", expressions.Select(x => "- " + x.Text.Replace("\n", "\n  ")));
        }
        else
        {
            return string.Join(spaces ? " | " : "|", expressions.Select(x => x.Text));
        }
    }
}