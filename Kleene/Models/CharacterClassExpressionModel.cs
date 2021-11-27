namespace Kleene.Models;

public class CharacterClassExpressionModel : IModel<CharacterClassExpression>
{
    public CharacterClassModel? CharacterClass { get; set; }

    public CharacterClassExpression Convert()
    {
        if (CharacterClass is null)
            throw new InvalidOperationException();

        return new(CharacterClass.Convert());
    }
}