namespace AdventOfCode.Puzzles.Y2015.Days.Day07;

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