using System;
using System.Collections.Generic;

namespace Kleene;

public class CharacterClassExpression : Expression
{
    public CharacterClass CharacterClass { get; }

    public CharacterClassExpression(CharacterClass characterClass)
    {
        CharacterClass = characterClass;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        if (!context.Local.Consuming)
        {
            throw new InvalidOperationException("Character classes cannot be used without input.");
        }

        if (context.Local.IsAtEnd)
        {
            yield break;
        }

        var c = context.Local.Input[context.Local.Index];
        if (CharacterClass.Accepts(c))
        {
            context.Consume(1);
            context.Produce(c.ToString());
            yield return new(c.ToString());
            context.Unproduce();
            context.Unconsume(1);
        }
    }
}
