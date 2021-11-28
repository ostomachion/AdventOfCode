using AdventOfCode.Helpers.DataStructures;

namespace AdventOfCode.Puzzles.Y2015.Days;

public class Day07 : Day
{
    public override Output Part1()
    {
        var test = Input;
        var input = Input.Lines().Parse<Line>(@"
            (
                - NOT ' ' \w+:V ::Not
                - \w+:L ' ' (AND|OR|LSHIFT|RSHIFT):Op ' ' \w+:R ::Bin
                - \w+:Value ::Ref
            )
            ' -> ' \w+:Dest
        ").ToList();
        Line.Reset();
        Line.Lines = input;
        return input.First(x => x.Dest == "a")!.Compute();
    }

    public override Output Part2()
    {
        var input = Input.Lines().Parse<Line>(@"
            (
                - NOT ' ' \w+:V ::Not
                - \w+:L ' ' (AND|OR|LSHIFT|RSHIFT):Op ' ' \w+:R ::Bin
                - \w+:Value ::Ref
            )
            ' -> ' \w+:Dest
        ").ToList();
        Line.Reset();
        Line.Lines = input;
        var a = input.First(x => x.Dest == "a")!.Compute();

        Line.Reset();
        Line.Lines.RemoveAll(x => x.Dest == "b");
        Line.Lines.Add(new Ref(a.ToString(), "b"));

        return input.First(x => x.Dest == "a")!.Compute();
    }
}

public abstract record Line(string Dest)
{
    public static List<Line> Lines = null!;
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
