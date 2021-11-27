namespace Kleene.Models;

public class AnchorModel : IModel<Anchor>
{
    public AnchorType? Type { get; set; }
    public CharacterClassModel? CharacterClass { get; set; }
    public bool Negated { get; set; }

    public Anchor Convert()
    {
        if (Type is null || CharacterClass is null)
            throw new InvalidOperationException();

        return new(Type.Value, CharacterClass.Convert(), Negated);
    }
}