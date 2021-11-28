using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Puzzles.Y2015.Days.Day04;

public class Day04 : Day
{
    public override Output Part1()
    {
        var i = 0;
        while (true)
        {
            var bytes = Encoding.UTF8.GetBytes(Input + i.ToString());
            var result = MD5.HashData(bytes).ToHexString();
            if (result.StartsWith("00000"))
            {
                return i;
            }

            i++;
        };
    }

    public override Output Part2()
    {
        var i = 0;
        while (true)
        {
            var bytes = Encoding.UTF8.GetBytes(Input + i.ToString());
            var result = MD5.HashData(bytes).ToHexString();
            if (result.StartsWith("000000"))
            {
                return i;
            }

            i++;
        };
    }
}
