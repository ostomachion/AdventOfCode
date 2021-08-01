using System.Security.Cryptography;
using System;
using System.Linq;
using AdventOfCode.Base;
using AdventOfCode.Helpers;
using AdventOfCode.Helpers.Cartesian;
using AdventOfCode.Helpers.Extensions;

namespace AdventOfCode.Y2015.Days
{
    public class Day04 : Day
    {
        public override Output Part1()
        {
            var i = 0;
            while (true)
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(Input + i.ToString());
                var result = MD5.HashData(bytes).ToHexString();
                if (result.StartsWith("00000"))
                    return i;
                i++;
            };
        }

        public override Output Part2()
        {
            throw new NotImplementedException();
        }
    }
}