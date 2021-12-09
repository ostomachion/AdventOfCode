namespace AdventOfCode.Puzzles.Y2015.Days.Day07;

public abstract record Line(string Dest)
{
    public static List<Line> Lines { get; set; } = null!;
    private static readonly Dictionary<string, ushort> memo = new();
    public abstract ushort Compute();
    public static ushort Resolve(string v)
    {
        if (!memo.ContainsKey(v))
            memo.Add(v, ushort.TryParse(v, out var i) ? i : Lines.First(x => x.Dest == v).Compute());
        return memo[v];
    }
    public static void Reset() => memo.Clear();
}

record Not(string V, string Dest) : Line(Dest)
{
    public override ushort Compute() => (ushort)~Resolve(V);
}

record Bin(string Op, string L, string R, string Dest) : Line(Dest)
{
    public override ushort Compute() => (ushort)(Op switch
    {
        "AND" => Resolve(L) & Resolve(R),
        "OR" => Resolve(L) | Resolve(R),
        "LSHIFT" => Resolve(L) << (int)Resolve(R),
        "RSHIFT" => Resolve(L) >> (int)Resolve(R),
        _ => throw new InvalidOperationException()
    });
}

record Ref(string Value, string Dest) : Line(Dest)
{
    public override ushort Compute() => Resolve(Value);
}