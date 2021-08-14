using System;
using System.Collections.Generic;

namespace Kleene
{
    public class AnchorExpression : Expression
    {
        public AnchorType Type { get; }
        public CharacterClass CharacterClass { get; }
        public bool Negated { get; }

        public AnchorExpression(AnchorType type, CharacterClass characterClass)
        {
            Type = type;
            CharacterClass = characterClass;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            var prev = context.Local.IsAtStart ? (char?)null : context.Local.Input[context.Local.Index - 1];
            var next = context.Local.IsAtEnd ? (char?)null : context.Local.Input[context.Local.Index];

            var prevMatches = prev is not null && CharacterClass.Accepts(prev.Value);
            var nextMatches = next is not null && CharacterClass.Accepts(next.Value);

            var pass = Type switch
            {
                AnchorType.Left => nextMatches && !prevMatches,
                AnchorType.Right => prevMatches && !nextMatches,
                AnchorType.Outer => prevMatches ^ nextMatches,
                AnchorType.Inner => prevMatches && nextMatches,
                _ => throw new InvalidOperationException()
            };

            if (pass ^ Negated)
                yield return new();
        }
    }
}
