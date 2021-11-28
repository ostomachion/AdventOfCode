namespace AdventOfCode.Puzzles.Y2015.Days;

public class Day02 : Day
{
    public override Output Part1()
    {
        var boxes = Input.Lines().Parse<Space3D>(@"\d+:I x \d+:J x \d+:K").ToArray();
        return boxes.Sum(box =>
        {
            var min = box!.Faces.Min(x => x.Area);
            var area = box.SurfaceArea + min;
            return area;
        });
    }

    public override Output Part2()
    {
        var boxes = Input.Lines().Parse<Space3D>(@"\d+:I x \d+:J x \d+:K").ToArray();
        return boxes.Sum(x => x!.Faces.Min(y => y.Perimeter) + x.Volume);
    }
}
