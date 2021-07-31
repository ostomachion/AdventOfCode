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
                var box = new SquareGrid(x.ParseInt().ToArray());
                var faces = box.Faces;
                var min = faces.Nodes.Min(x => x.Value.Volume);
                var area = box.SurfaceArea + min;
                return area;
            });
        }

        public override Output Part2()
        {
            var input = Input.Lines().Split('x');
            return input.Sum(x =>
            {
                var box = new SquareGrid(x.ParseInt().ToArray());
                var faces = box.Faces;
                var ribbon = faces.Nodes.Min(y => y.Value.SurfaceArea) + box.Volume;
                return ribbon;
            });
        }
    }
}