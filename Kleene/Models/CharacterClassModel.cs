namespace Kleene.Models;

public class CharacterClassModel : IModel<CharacterClass>
{
    public string? Characters { get; set; }
    public bool Negated { get; set; }

    public CharacterClass Convert()
    {
        if (Characters is null)
            throw new InvalidOperationException();

        return new(Characters, Negated);
    }
}
