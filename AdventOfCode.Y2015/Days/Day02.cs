using System;
using System.Linq;
using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Y2015.Days
{
    public class Day02 : Day
    {
        public override Output Part1()
        {
            var input = Input.Lines().Split('x');
            return input.Sum(x =>
            {
                var box = new Box(x.ParseInt().ToArray());
                var min = box.Faces.Min(x => x.Volume);
                var area = box.SurfaceArea + min;
                return area;
            });
        }

        public override Output Part2()
        {
            var input = Input.Lines().Split('x');
            return input.Sum(x =>
            {
                var box = new Box(x.ParseInt().ToArray());
                var ribbon = box.Faces.Min(y => y.SurfaceArea) + box.Volume;
                return ribbon;
            });
        }
    }
}