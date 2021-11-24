using AdventOfCode.Helpers.Cartesian.Boxes;
using AdventOfCode.Helpers.Extensions;
using System.Linq;

namespace AdventOfCode.Puzzles.Y2015.Days
{
    public class Day02 : Day
    {
        public override Output Part1()
        {
            var input = Input.Lines().Split('x');
            return input.Sum(x =>
            {
                var dim = x.ParseInt().ToArray();
                var box = Box.Make(dim[0], dim[1], dim[2]);
                var min = box.Faces.Min(x => x.Area);
                var area = box.SurfaceArea + min;
                return area;
            });
        }

        public override Output Part2()
        {
            var input = Input.Lines().Split('x');
            return input.Sum(x =>
            {
                var dim = x.ParseInt().ToArray();
                var box = Box.Make(dim[0], dim[1], dim[2]);
                var ribbon = box.Faces.Min(y => y.Perimeter) + box.Volume;
                return ribbon;
            });
        }
    }
}