using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day16;

public abstract record Packet(string Version, string Type, string Remainder = "")
{
    public long GetVersion() => Version.FromBase(2);
    public virtual long GetVersionSum() => Version.FromBase(2);
    public long GetType() => Type.FromBase(2);
    public abstract long GetValue();

    public static Packet Parse(string input)
    {
        return input.Parse<Packet>(@"(\d^3):Version
        (
            - (100):Type (1 (\d^4):Value)* (0 (\d^4):Value) ::LiteralPacket
            - (\d^3):Type (0:LengthType (\d^15):Length | 1:LengthType (\d^11):Length) ::OperatorPacket
        )

        \d*:Remainder
        ");
    }

    public virtual IEnumerable<Packet> Fix()
    {
        yield return this with { Remainder = "" };
        if (Remainder.All(c => c == '0'))
            yield break;
        var ps = Packet.Parse(Remainder).Fix();
        foreach (var p in ps)
            yield return p;
    }
}

public record LiteralPacket(string Version, string Type, List<string> Value = null, string Remainder = "") : Packet(Version, Type, Remainder)
{
    public override long GetValue()
    {
        return string.Join("", Value).FromBase(2);
    }
}

public record OperatorPacket(string Version, string Type, string LengthType, string Length, List<Packet> Operands = null, string Remainder = "") : Packet(Version, Type, Remainder)
{
    public override long GetVersionSum() => base.GetVersionSum() + Operands.Sum(x => x.GetVersionSum());

    public override long GetValue()
    {
        switch (Type.FromBase(2))
        {
            case 0: return Operands.Sum(x => x.GetValue());
            case 1: return Operands.Product(x => x.GetValue());
            case 2: return Operands.Min(x => x.GetValue());
            case 3: return Operands.Max(x => x.GetValue());
            case 5: return Operands[0].GetValue() > Operands[1].GetValue() ? 1 : 0;
            case 6: return Operands[0].GetValue() < Operands[1].GetValue() ? 1 : 0;
            case 7: return Operands[0].GetValue() == Operands[1].GetValue() ? 1 : 0;
            default: throw new InvalidOperationException();
        }
    }

    public override IEnumerable<Packet> Fix()
    {
        if (this.Operands is null)
        {
            var length = (int)Length.FromBase(2);
            if (LengthType == "0")
            {
                var sub = Packet.Parse(Remainder[..length]);
                var val = this with { Operands = sub.Fix().ToList(), Remainder = Remainder[length..] };
                foreach (var v in val.Fix())
                    yield return v;
            }
            else
            {
                var rem = Packet.Parse(Remainder).Fix();
                var val = this with { Operands = rem.Take(length).ToList(), Remainder = "" };
                yield return val;
                foreach (var r in rem.Skip(length))
                    yield return r;
            }
        }
        else
        {
            foreach (var v in base.Fix())
                yield return v;
        }
    }
}

public class Day16 : Day
{
    public override string? TestInput => @"C200B40A82";

    public override Output? TestOutputPart1 => 31;
    public override Output? TestOutputPart2 => 3;

    public override Output Part1()
    {
        var input = string.Join("", Input.Select(c => c.ToString().ToLower().FromBase(16).ToBase(2).PadLeft(4, '0')).ToList());

        var packet = input.Parse<Packet>(@"(\d^3):Version
        (
            - (100):Type (1 (\d^4):Value)* (0 (\d^4):Value) ::LiteralPacket
            - (\d^3):Type (0:LengthType (\d^15):Length | 1:LengthType (\d^11):Length) \d*:Remainder ::OperatorPacket
        )

        0*
");

        var test = packet.Fix().ToList();

        return test.Sum(x => x.GetVersionSum());
    }

    public override Output Part2()
    {
        var input = string.Join("", Input.Select(c => c.ToString().ToLower().FromBase(16).ToBase(2).PadLeft(4, '0')).ToList());

        var packet = input.Parse<Packet>(@"(\d^3):Version
        (
            - (100):Type (1 (\d^4):Value)* (0 (\d^4):Value) ::LiteralPacket
            - (\d^3):Type (0:LengthType (\d^15):Length | 1:LengthType (\d^11):Length) \d*:Remainder ::OperatorPacket
        )

        0*
");

        var test = packet.Fix().ToList();

        return test.Single().GetValue();
    }
}
