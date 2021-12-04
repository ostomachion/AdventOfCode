namespace AdventOfCode.Puzzles.Y2021.Days.Day03;

public class Day03 : Day
{
    public override Output Part1()
    {
        var value = Input.Lines();

        var gamma = "";
        var epsilon = "";
        for (int i = 0; i < value[0].Length; i++)
        {
            var check = value.Select(x => x[i]).ToArray().Mode().Contains('0');
            gamma += check ? "1" : "0";
            epsilon += check ? "0" : "1";
        }

        return gamma.FromBinary() * epsilon.FromBinary();
    }

    public override Output Part2()
    {
        var value = Input.Lines();

        var i = 0;
        var o2 = value;
        while (o2.Length != 1)
        {
            var check = o2.Select(x => x[i]).ToArray().Mode().Contains('1');
            o2 = o2.Where(x => x[i] == (check ? '1' : '0')).ToArray();
            i++;
        }

        var co2 = value;
        i = 0;
        while (co2.Length != 1)
        {
            var check = co2.Select(x => x[i]).ToArray().Mode().Contains('1');
            co2 = co2.Where(x => x[i] == (check ? '0' : '1')).ToArray();
            i++;
        }

        return o2.Single().FromBinary() * co2.Single().FromBinary();
    }
}
