namespace Kleene;

public class KList<T> : List<T>
{
    public List<T> Items
    {
        get => this;
        set
        {
            Clear();
            AddRange(value);
        }
    }
}
