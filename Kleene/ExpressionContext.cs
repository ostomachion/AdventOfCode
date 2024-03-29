namespace Kleene;

public class ExpressionContext
{
    public ExpressionLocalContext Local { get; set; }
    public CaptureTree CaptureTree { get; } = new();
    public Stack<StackFrame> CallStack { get; } = new();
    public bool Ratchet
    {
        get => CallStack.Peek().Ratchet;
        set => CallStack.Peek().Ratchet = value;
    }
    public IEnumerable<string> Usings => CallStack.SelectMany(x => x.Usings).Distinct();

    public ExpressionContext(string input)
    {
        input = input.ReplaceLineEndings("\n");
        Local = new(input);
        CallStack.Push(new StackFrame("[root]"));
    }

    public void Consume(int length) => Local.Consume(length);

    public void Unconsume(int length) => Local.Unconsume(length);

    public void Produce(string value) => Local.Produce(value);

    public void Unproduce() => Local.Unproduce();

    public void DefineFunction(string name, Expression expression) => CallStack.Peek().Functions.Define(name, expression);
    public void UndefineFunction(string name) => CallStack.Peek().Functions.Undefine(name);
    public Expression? GetFunction(string name) => CallStack.Select(x => x.Functions[name]).OfType<Expression>().FirstOrDefault();

    public Type ResolveTypeName(string name)
    {
        if (ParseTypeKeyword(name) is Type value)
            return value;

        var types = Usings.SelectMany(x => AppDomain.CurrentDomain.GetAssemblies().Select(y => y.GetType(x + "." + name))).OfType<Type>().ToList();
        if (Type.GetType(name) is Type type)
            types.Add(type);

        return types.Count switch
        {
            0 => throw new Exception($"The type name '{name}' could not be found."),
            1 => types[0],
            _ => throw new Exception($"'{name}' is ambiguous between '{types[0].FullName}' and '{types[0].FullName}'."),
        };

        static Type? ParseTypeKeyword(string name) => name switch
        {
            "bool" => typeof(bool),
            "byte" => typeof(byte),
            "sbyte" => typeof(sbyte),
            "char" => typeof(char),
            "decimal" => typeof(decimal),
            "double" => typeof(double),
            "float" => typeof(float),
            "int" => typeof(int),
            "uint" => typeof(uint),
            "nint" => typeof(nint),
            "nuint" => typeof(nuint),
            "long" => typeof(long),
            "ulong" => typeof(ulong),
            "short" => typeof(short),
            "ushort" => typeof(ushort),
            "object" => typeof(object),
            "string" => typeof(string),
            _ => null
        };
    }
}