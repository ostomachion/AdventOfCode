namespace Kleene;

public class CharacterClassExpression : Expression
{
    internal class Model : IModel<CharacterClassExpression>
    {
        public IModel<CharacterClass>? CharacterClass { get; set; }

        public CharacterClassExpression Convert()
        {
            if (CharacterClass is null)
                throw new InvalidOperationException();

            return new(CharacterClass.Convert());
        }
    }

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
