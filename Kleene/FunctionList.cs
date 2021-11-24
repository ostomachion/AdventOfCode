using System.Collections;

namespace Kleene;

public class FunctionList : IEnumerable<KeyValuePair<string, Expression>>
{
    private readonly Dictionary<string, Stack<Expression>> functions = new();

    public Expression? this[string name] => functions.TryGetValue(name, out var value) ? value.Peek() : null;

    public void Define(string name, Expression expression)
    {
        if (!functions.ContainsKey(name))
        {
            functions.Add(name, new());
        }

        functions[name].Push(expression);
    }

    public void Undefine(string name)
    {
        functions[name].Pop();

        if (!functions[name].Any())
        {
            functions.Remove(name);
        }
    }

    public IEnumerator<KeyValuePair<string, Expression>> GetEnumerator()
    {
        return functions
            .Select(x => new KeyValuePair<string, Expression>(x.Key, x.Value.Peek()))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
