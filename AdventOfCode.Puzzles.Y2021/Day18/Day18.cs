using System.Text;

namespace AdventOfCode.Puzzles.Y2021.Days.Day18;

public abstract class Fish
{
    public virtual int Depth { get; set; }
    public Fish? Parent { get; set; }
    public abstract FishN LeftMost { get; }
    public abstract FishN RightMost { get; }
    public abstract long Magnitude { get; }
    public abstract bool Reduce(bool pair);
    public abstract Fish Clone();
    public int FullReduce()
    {
        var value = 0;
        while (Reduce(true) || Reduce(false))
            value++;
        return value;
    }

    public FishN? GetLeft()
    {
        return Parent is FishPair p ? this.Equals(p.Right) ? p.Left.RightMost : p.GetLeft() : null;
    }

    public FishN? GetRight()
    {
        return Parent is FishPair p ? this.Equals(p.Left) ? p.Right.LeftMost : p.GetRight() : null;
    }

    public void ReplaceWith(Fish other)
    {
        if (Parent is not FishPair p)
            throw new InvalidOperationException();

        if (this.Equals(p.Left))
            p.Left = other;
        else if (this.Equals(p.Right))
            p.Right = other;
        else
            throw new InvalidOperationException();
    }
}

public class FishPair : Fish
{
    private int depth;
    private Fish left;
    private Fish right;
    public override int Depth
    {
        get => depth;
        set
        {
            depth = value;
            left.Depth = value + 1;
            right.Depth = value + 1;
        }
    }
    public Fish Left
    {
        get => left;
        set
        {
            left = value;
            left.Depth = Depth + 1;
            left.Parent = this;
        }
    }
    public Fish Right
    {
        get => right;
        set
        {
            right = value;
            right.Depth = Depth + 1;
            right.Parent = this;
        }
    }

    public override FishN LeftMost => left is FishPair p ? p.LeftMost : (FishN)left;
    public override FishN RightMost => right is FishPair p ? p.RightMost : (FishN)right;

    public override long Magnitude => 3 * left.Magnitude + 2 * right.Magnitude;

    public override bool Reduce(bool pair)
    {
        if (pair && Depth == 4)
        {
            var l = this.GetLeft();
            if (l is not null)
                l.Value += ((FishN)this.left).Value;

            var r = this.GetRight();
            if (r is not null)
                r.Value += ((FishN)this.right).Value;

            this.ReplaceWith(new FishN { Value = 0 });
            return true;
        }
        else
        {
            return left.Reduce(pair) || right.Reduce(pair);
        }
    }

    public static FishPair operator +(FishPair left, FishPair right)
    {
        return new FishPair { Left = left.Clone(), Right = right.Clone() };
    }

    public override string ToString()
    {
        return $"[{left},{right}]";
    }

    public override Fish Clone()
    {
        return new FishPair { Left = this.Left.Clone(), Right = this.Right.Clone() };
    }
}

public class FishN : Fish
{
    public long Value { get; set; }

    public override FishN LeftMost => this;

    public override FishN RightMost => this;

    public override long Magnitude => Value;

    public override Fish Clone()
    {
        return new FishN { Value = this.Value };
    }

    public override bool Reduce(bool pair)
    {
        if (!pair && Value > 9)
        {
            this.ReplaceWith(new FishPair { Left = new FishN { Value = Value / 2 }, Right = new FishN { Value = Value / 2 + Value % 2 } });
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

public class Day18 : Day
{
    public override string? TestInput => @"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]";

    public override Output? TestOutputPart1 => 4140;
    public override Output? TestOutputPart2 => 3993;

    public override Output Part1()
    {
        var pattern = @"
    <fish> { <pair> ::FishPair | <n> ::FishN }
    <pair> { '[<]' <fish>:Left ',' <fish>:Right '[>]' }
    <n> { (\d+):Value }

    <fish>
";
        var input = Input.Lines().Parse<FishPair>(pattern).ToList();

        var value = input[0];
        for (var i = 1; i < input.Count; i++)
        {
            value += input[i];
            value.FullReduce();
        }

        return value.Magnitude;
    }

    public override Output Part2()
    {
        var pattern = @"
    <fish> { <pair> ::FishPair | <n> ::FishN }
    <pair> { '[<]' <fish>:Left ',' <fish>:Right '[>]' }
    <n> { (\d+):Value }

    <fish>
";

        var max = 0L;
        var input = Input.Lines().Parse<FishPair>(pattern).ToList();
        for (var i = 0; i < input.Count; i++)
        {
            Console.WriteLine(i);
            for (int j = 0; j < input.Count; j++)
            {
                if (i == 0)
                    continue;
                var sum = input[i] + input[j];
                sum.FullReduce();
                max = Math.Max(max, sum.Magnitude);
            }
        }

        return max;
    }
}
