using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System;
using System.Linq;
using AdventOfCode.Puzzles;
using AdventOfCode.Helpers;
using AdventOfCode.Helpers.Cartesian;
using AdventOfCode.Helpers.Extensions;
using AdventOfCode.Helpers.Numeric;

namespace AdventOfCode.Puzzles.Y2015.Days
{
    public class Day06 : Day
    {
        //public record MatchValue(int Toggle, Point Start, Point End);
        public override Output Part1()
        {
            // var input = Input.Lines().Parse<MatchValue>(
            //     new(@"turn on (\d+),(\d+) through (\d+),(\d+)", new Func<Match, MatchValue>(m => new(1,
            //        new(m.Groups[1].Value.ParseInt(), m.Groups[2].Value.ParseInt()),
            //        new(m.Groups[3].Value.ParseInt(), m.Groups[4].Value.ParseInt())))),
            //     new(@"toggle (\d+),(\d+) through (\d+),(\d+)", new Func<Match, MatchValue>(m => new(0,
            //        new(m.Groups[1].Value.ParseInt(), m.Groups[2].Value.ParseInt()),
            //        new(m.Groups[3].Value.ParseInt(), m.Groups[4].Value.ParseInt())))),
            //     new(@"turn off (\d+),(\d+) through (\d+),(\d+)", new Func<Match, MatchValue>(m => new(-1,
            //        new(m.Groups[1].Value.ParseInt(), m.Groups[2].Value.ParseInt()),
            //        new(m.Groups[3].Value.ParseInt(), m.Groups[4].Value.ParseInt()))))
            // );

            // var grid = new Grid<bool>(1000, 1000);
            // foreach (var item in input)
            // {
            //     for (var x = item!.Start.X; x <= item.End.X; x++)
            //     {

            //         for (var y = item.Start.Y; y <= item.End.Y; y++)
            //         {
            //             switch (item.Toggle)
            //             {
            //                 case 1:
            //                     grid[(x, y)] = true;
            //                     break;
            //                 case 0:
            //                     grid[(x, y)] = !grid[(x, y)];
            //                     break;
            //                 case -1:
            //                     grid[(x, y)] = false;
            //                     break;
            //             }
            //         }
            //     }
            // }
            // return grid.Values.Count(x => x);
            throw new NotImplementedException();
        }

        public override Output Part2()
        {
            // var input = Input.Lines().Skip(0).Parse<MatchValue>(
            //     new(@"turn on (\d+),(\d+) through (\d+),(\d+)", new Func<Match, MatchValue>(m => new(1,
            //        new(m.Groups[1].Value.ParseInt(), m.Groups[2].Value.ParseInt()),
            //        new(m.Groups[3].Value.ParseInt(), m.Groups[4].Value.ParseInt())))),
            //     new(@"toggle (\d+),(\d+) through (\d+),(\d+)", new Func<Match, MatchValue>(m => new(0,
            //        new(m.Groups[1].Value.ParseInt(), m.Groups[2].Value.ParseInt()),
            //        new(m.Groups[3].Value.ParseInt(), m.Groups[4].Value.ParseInt())))),
            //     new(@"turn off (\d+),(\d+) through (\d+),(\d+)", new Func<Match, MatchValue>(m => new(-1,
            //        new(m.Groups[1].Value.ParseInt(), m.Groups[2].Value.ParseInt()),
            //        new(m.Groups[3].Value.ParseInt(), m.Groups[4].Value.ParseInt()))))
            // );
            
            // var grid = new Grid<Natural>(1000, 1000);
            // foreach (var item in input)
            // {
            //     for (var x = item!.Start.X; x <= item.End.X; x++)
            //     {

            //         for (var y = item.Start.Y; y <= item.End.Y; y++)
            //         {
            //             switch (item.Toggle)
            //             {
            //                 case 1:
            //                     grid[(x, y)] = grid[(x, y)] + 1;
            //                     break;
            //                 case 0:
            //                     grid[(x, y)] = grid[(x, y)] + 2;
            //                     break;
            //                 case -1:
            //                     grid[(x, y)] = grid[(x, y)] - 1;
            //                     break;
            //             }
            //         }
            //     }
            // }
            // return grid.Values.Sum(x => (long)x.Value);
            throw new NotImplementedException();
        }
    }
}